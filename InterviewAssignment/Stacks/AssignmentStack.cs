using System.Collections.Generic;
using InterviewAssignmnet.CustomResources.DocumentDB;
using InterviewAssignmnet.CustomResources.ManagedIdentity;
using InterviewAssignmnet.CustomResources.Network;
using InterviewAssignmnet.CustomResources.Resources;
using InterviewAssignmnet.CustomResources.Insights;
using InterviewAssignmnet.CustomResources.Storage;
using InterviewAssignmnet.CustomResources.Web;
using InterviewAssignmnet.Utility;

namespace InterviewAssignmnet.Stacks;

public class AssignmentStack : Pulumi.Stack
{
    private readonly string funcAppSuffix;
    private readonly string appServiceSuffix;
    private readonly Pulumi.Output<Pulumi.AzureNative.Authorization.GetClientConfigResult> clientConfig;
    private readonly AssignmentResourceGroup rg;
    private AssignmentVirtualNetwork vnet;
    private AssignmentNetworkSecurityGroup funcAppSnetNsg;
    private AssignmentNetworkSecurityGroup appServiceSnetNsg;
    private AssignmentSubnet funcAppSubnet;
    private AssignmentSubnet appServiceSubnet;
    private AssignmentWebApp funcApp;
    private AssignmentDiagnosticSetting funcAppDiagnosticSettings;
    private AssignmentWebAppPrivateEndpointConnection funcAppPrivateEndpointConnection;
    private AssignmentPrivateEndpoint funcAppPrivateEndpoint;
    private AssignmentAppServicePlan funcAppAsp;
    private AssignmentUserAssignedIdentity funcAppManagedIdentity;
    private AssignmentWebApp appService;
    private AssignmentDiagnosticSetting appServiceDiagnosticSettings;
    private AssignmentAppServicePlan appServiceAsp;
    private AssignmentUserAssignedIdentity appServiceManagedIdentity;
    private AssignmentStorageAccount diagnosticsStorage;
    private AssignmentBlobContainer blobContainer;
    private AssignmentDatabaseAccount cosmosDb;
    private AssignmentCosmosPrivateEndpointConnection cosmosDbPrivateEndpointConnection;
    private AssignmentPrivateEndpoint cosmosPrivateEndpoint;

    public AssignmentStack()
    {
        funcAppSuffix = "func-app";
        appServiceSuffix = "app-service";

        clientConfig = Pulumi.AzureNative.Authorization.GetClientConfig.Invoke();
        rg = new AssignmentResourceGroup("main", Config.Azure.Location);

        DeployNetworks();
        DeployStorage();
        DeployFuncApp();
        DeployAppService();
    }

    private void DeployStorage()
    {
        diagnosticsStorage = new AssignmentStorageAccount("diagnostics", rg);
        blobContainer = new AssignmentBlobContainer("diagnostics", rg, diagnosticsStorage);

        cosmosDb = new AssignmentDatabaseAccount("", rg, new List<string>{Config.Azure.Location});
        cosmosDbPrivateEndpointConnection = new AssignmentCosmosPrivateEndpointConnection("cosmos", rg, cosmosDb);
        cosmosPrivateEndpoint = new AssignmentPrivateEndpoint("cosmos", rg, appServiceSubnet, cosmosDbPrivateEndpointConnection);
    }

    private void DeployAppService()
    {
        appServiceAsp = new AssignmentAppServicePlan(
            appServiceSuffix,
            rg,
            CustomResources.Builders.Web.AspSku.AppServiceStandard
        );

        appServiceManagedIdentity = new AssignmentUserAssignedIdentity(appServiceSuffix, rg);

        var roleAssignment2 = new Pulumi.AzureNative.Authorization.RoleAssignment(
            "app-service-cosmos-role-assignment",
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
            appServiceSubnet
        );

        appServiceDiagnosticSettings = new AssignmentDiagnosticSetting(appServiceSuffix, diagnosticsStorage, appService);
    }

    private void DeployNetworks()
    {
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
    }

    private void DeployFuncApp()
    {
        funcAppAsp = new AssignmentAppServicePlan(funcAppSuffix, rg, CustomResources.Builders.Web.AspSku.FunctionApp);

        funcAppManagedIdentity = new AssignmentUserAssignedIdentity(funcAppSuffix, rg);

        var roleAssignment = new Pulumi.AzureNative.Authorization.RoleAssignment(
            "func-app-storage-role-assignment",
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
            diagnosticsStorage
        );

        funcAppDiagnosticSettings = new AssignmentDiagnosticSetting(funcAppSuffix, diagnosticsStorage, funcApp);

        funcAppPrivateEndpointConnection = new AssignmentWebAppPrivateEndpointConnection(funcAppSuffix, rg, funcApp);
        funcAppPrivateEndpoint = new AssignmentPrivateEndpoint(funcAppSuffix, rg, funcAppSubnet, funcAppPrivateEndpointConnection);
    }
}
