using System.Diagnostics;
using System.Threading;
using InterviewAssignmnet.CustomResources.ManagedIdentity;
using InterviewAssignmnet.CustomResources.Network;
using InterviewAssignmnet.CustomResources.Resources;
using InterviewAssignmnet.CustomResources.Storage;
using InterviewAssignmnet.CustomResources.Web;
using InterviewAssignmnet.Utility;
using Pulumi.AzureNative.Storage;

namespace InterviewAssignmnet.Stacks;

public class AssignmentStack : Pulumi.Stack
{
    private readonly string baseResourceName;
    private readonly string funcAppSuffix;
    private readonly string appServiceSuffix;
    private readonly ResourceNames resourceNames;

    //Vnet
    //snet x2
    //private endpoing
    //function app
    //app service
    //asp x2
    //cosmos db
    //cosmos collection
    //storage account
    //blob container
    //user assigned managed identity

    private readonly AssignmentResourceGroup rg;
    private AssignmentVirtualNetwork vnet;
    private AssignmentNetworkSecurityGroup funcAppSnetNsg;
    private AssignmentNetworkSecurityGroup appServiceSnetNsg;
    private AssignmentSubnet funcAppSubnet;
    private AssignmentSubnet appServiceSubnet;
    private AssignmentWebApp funcApp;
    private AssignmentAppServicePlan funcAppAsp;
    private AssignmentUserAssignedIdentity funcAppManagedIdentity;
    private AssignmentWebApp appService;
    private AssignmentAppServicePlan appServiceAsp;
    private AssignmentUserAssignedIdentity appServiceManagedIdentity;
    private AssignmentStorageAccount appServiceStorage;
    private AssignmentStorageAccount diagnosticsStorage;
    private AssignmentBlobContainer blobContainer;

    public AssignmentStack()
    {
        funcAppSuffix = "func-app";
        appServiceSuffix = "app-service";

        rg = new AssignmentResourceGroup("main", Config.Azure.Location);

        this.vnet = new AssignmentVirtualNetwork("main", rg);

        funcAppSnetNsg = new AssignmentNetworkSecurityGroup(funcAppSuffix, rg);

        funcAppSubnet = new AssignmentSubnet(
            funcAppSuffix,
            rg,
            vnet,
            funcAppSnetNsg,
            Config.Network.FuncAppSubnetAddressSpace
        );

        appServiceSnetNsg = new AssignmentNetworkSecurityGroup(appServiceSuffix, rg);

        appServiceSubnet = new AssignmentSubnet(
            appServiceSuffix,
            rg,
            vnet,
            appServiceSnetNsg,
            Config.Network.AppServiceSubnetAddressSpace
        );

        DeployWebApps();
    }

    private void DeployWebApps()
    {
        diagnosticsStorage = new AssignmentStorageAccount("diagnostics", rg);
        blobContainer = new AssignmentBlobContainer("diagnostics", rg, diagnosticsStorage);

        funcAppAsp = new AssignmentAppServicePlan(
            funcAppSuffix,
            rg,
            CustomResources.Builders.Web.AspSku.FunctionsPremium
        );

        funcAppManagedIdentity = new AssignmentUserAssignedIdentity(funcAppSuffix, rg);

        var clientConfig = Pulumi.AzureNative.Authorization.GetClientConfig.Invoke();
        var roleAssignment = new Pulumi.AzureNative.Authorization.RoleAssignment(
            $"{funcAppManagedIdentity.Name}-roleAssignment",
            new()
            {
                PrincipalId = funcAppManagedIdentity.PrincipalId,
                PrincipalType = Pulumi.AzureNative.Authorization.PrincipalType.User,
                RoleAssignmentName = $"{funcAppManagedIdentity.Name}-storage",
                RoleDefinitionId = clientConfig.Apply(clientConfig =>
                    $"/subscriptions/{clientConfig.SubscriptionId}/providers/Microsoft.Authorization/roleDefinitions/ba92f5b4-2d11-453d-a403-e96b0029c9fe"
                ),
                Scope = diagnosticsStorage.Id,
            }
        );

        funcApp = new AssignmentWebApp(
            funcAppSuffix,
            rg,
            funcAppAsp,
            CustomResources.Builders.Web.WebAppKind.FunctionApp,
            diagnosticsStorage,
            funcAppSubnet,
            blobContainer
        );

        appServiceAsp = new AssignmentAppServicePlan(
            appServiceSuffix,
            rg,
            CustomResources.Builders.Web.AspSku.AppServiceStandard
        );

        appServiceManagedIdentity = new AssignmentUserAssignedIdentity(appServiceSuffix, rg);

        var roleAssignment2 = new Pulumi.AzureNative.Authorization.RoleAssignment(
            $"{appServiceManagedIdentity.Name}-roleAssignment",
            new()
            {
                PrincipalId = appServiceManagedIdentity.PrincipalId,
                PrincipalType = Pulumi.AzureNative.Authorization.PrincipalType.User,
                RoleAssignmentName = $"{appServiceManagedIdentity.Name}-cosmos",
                RoleDefinitionId = clientConfig.Apply(clientConfig =>
                    $"/subscriptions/{clientConfig.SubscriptionId}/providers/Microsoft.Authorization/roleDefinitions/fbdf93bf-df7d-467e-a4d2-9458aa1360c8"
                ),
                Scope = cosmosDb.Id,
            }
        );

        appService = new AssignmentWebApp(
            appServiceSuffix,
            rg,
            appServiceAsp,
            CustomResources.Builders.Web.WebAppKind.WebApp,
            diagnosticsStorage,
            appServiceSubnet,
            blobContainer
        );
    }
}
