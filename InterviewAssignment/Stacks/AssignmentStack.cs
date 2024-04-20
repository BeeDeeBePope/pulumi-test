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
    private AssignmentStorageAccount funcAppStorage;
    private BlobContainer blobContainer;

    public AssignmentStack()
    {
        funcAppSuffix = "-func-app";
        appServiceSuffix = "-app-service";

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
        funcAppAsp = new AssignmentAppServicePlan(funcAppSuffix, rg);

        funcAppManagedIdentity = new AssignmentUserAssignedIdentity(baseResourceName, rg);
        funcAppStorage = new AssignmentStorageAccount(funcAppSuffix, rg);

        funcApp = new AssignmentWebApp(
            baseResourceName,
            rg,
            funcAppAsp,
            funcAppStorage,
            funcAppSubnet
        );

        // var funcAppStorageArgs = new AssignmentStorageAccountArgs(rg.GetName(), resourceNames.FuncAppStorageAccount);

        // var blobContainerArgs = new AssignmentBlobContainerArgs();
        // blobContainer = new AssignmentBlobContainer(resourceNames.FuncAppDiagnosticsSettingsContainer, blobContainerArgs);

        // var funcAppAspArgs = new AssignmentAppServicePlanArgs(rg.GetName(), resourceNames.FuncAppAspName, "Linux", AspSku.FunctionApp);

        // var funcAppSettings = new List<NameValuePairArgs>()
        // {
        //     new NameValuePairArgs
        //     {
        //         Name = "AzureWebJobsStorage",
        //         Value = funcAppStorage.GetConnectionString(),
        //     },
        // };
        // var funcAppDiagnosticSettings = new WebAppDiagnosticLogsConfiguration("FuncAppDiagnosticsSettings", new WebAppDiagnosticLogsConfigurationArgs
        // {
        //     Name = resourceNames.FuncAppDiagnosticsSettings,
        //     ResourceGroupName = rg.GetName(),
        //     DetailedErrorMessages = new EnabledConfigArgs
        //     {
        //         Enabled = false,
        //     },
        //     FailedRequestsTracing = new EnabledConfigArgs
        //     {
        //         Enabled = false,
        //     },
        //     ApplicationLogs = new ApplicationLogsConfigArgs
        //     {
        //         AzureBlobStorage = new AzureBlobStorageApplicationLogsConfigArgs
        //         {
        //             Level = LogLevel.Information,
        //             RetentionInDays = 7,
        //             SasUrl = blobContainer
        //         }
        //     },
        //     HttpLogs = ,
        //     Kind = ""
        // });
        // var funcAppArgs = new AssignmentWebAppArgs(rg.GetName(), funcAppAsp.GetId(), WebAppKind.FunctionApp, funcAppManagedIdentity.GetId(), funcAppSettings);

        // var appServiceAspArgs = new AssignmentAppServicePlanArgs(rg.GetName(), resourceNames.AppServiceAspName, "Linux", AspSku.AppServiceFree);
        // appServiceAsp = new AssignmentAppServicePlan(resourceNames.AppServiceAspName, appServiceAspArgs.GetAspArgs());

        // var appServiceManagedIndentityArgs = new AssignmentUserAssignedIdentityArgs(rg.GetName(), resourceNames.AppServiceManagedIdentityName);
        // appServiceManagedIdentity = new AssignmentUserAssignedIdentity(resourceNames.AppServiceManagedIdentityName, appServiceManagedIndentityArgs.GetUserAssignedIdentityArgs());

        // var appServiceArgs = new AssignmentWebAppArgs(rg.GetName(), appServiceAsp.GetId(), WebAppKind.WebApp, appServiceManagedIdentity.GetId(), new List<Pulumi.AzureNative.Web.Inputs.NameValuePairArgs>());
        // appService = new AssignmentWebApp(resourceNames.AppServiceName, appServiceArgs.GetWebAppArgs());
    }
}
