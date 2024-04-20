using Pulumi;
using InterviewAssignmnet.CustomResources.Network;
using InterviewAssignmnet.CustomResources.Resources;
using System.Collections.Generic;
using Pulumi.AzureNative.Resources;
using InterviewAssignmnet.CustomResources.Web;
using InterviewAssignmnet.CustomResources.Storage;
using InterviewAssignmnet.CustomResources.ManagedIdentity;
using Pulumi.AzureNative.Web.Inputs;
using Pulumi.AzureNative.Web;
using Pulumi.AzureNative.Storage;

namespace InterviewAssignmnet.Stacks;
public class AssignmentStack : Stack
{
    private readonly string baseResourceName;
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

    private AssignmentResourceGroup rg;
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
        var config = new Config("general");
        var azureConfig = new Config("azure-native");

        var location = azureConfig.Require("location");

        baseResourceName = $"assignment-{location}";

        resourceNames = new ResourceNames(location);

        rg = new AssignmentResourceGroup(resourceNames.RgName, new ResourceGroupArgs
        {
            ResourceGroupName = resourceNames.RgName,
            Location = location,
        });

        DeployVirtualNetworks();

        DeployWebApps();
    }

    private void DeployVirtualNetworks()
    {
        // var networkConfig = new Config("network");
        // var vnetAddressSpace = networkConfig.Require("vnetAddressSpace");
        // var funcAppSubnetAddressSpace = networkConfig.Require("funcAppSubnetAddressSpace");
        // var appServiceSubnetAddressSpace = networkConfig.Require("appServiceSubnetAddressSpace");

        vnet = new AssignmentVirtualNetwork(baseResourceName, rg);

        var defaultNsgRules = new AssignmentNetworkSecurityRules();

        var funcAppNsgArgs = new AssignmentNetworkSecurityGroupArgs(rg.GetName(), resourceNames.FuncAppNsgName, defaultNsgRules.GetRules());
        funcAppSnetNsg = new AssignmentNetworkSecurityGroup(resourceNames.FuncAppNsgName, funcAppNsgArgs.GetNetworkSecurityGroupArgs());

        var funcAppSubnetArgs = new AssignmentSubnetArgs(rg.GetName(), vnet.GetName(), resourceNames.FuncAppSnetName, funcAppSubnetAddressSpace);
        funcAppSubnet = new AssignmentSubnet(resourceNames.FuncAppSnetName, funcAppSubnetArgs.GetSubnetArgs());

        var appServiceNsgArgs = new AssignmentNetworkSecurityGroupArgs(rg.GetName(), resourceNames.AppServiceNsgName, defaultNsgRules.GetRules());
        appServiceSnetNsg = new AssignmentNetworkSecurityGroup(resourceNames.AppServiceNsgName, appServiceNsgArgs.GetNetworkSecurityGroupArgs());

        var appServiceSubnetArgs = new AssignmentSubnetArgs(rg.GetName(), vnet.GetName(), resourceNames.AppServiceSnetName, appServiceSubnetAddressSpace);
        appServiceSubnet = new AssignmentSubnet(resourceNames.AppServiceSnetName, appServiceSubnetArgs.GetSubnetArgs());
    }

    private void DeployWebApps()
    {
        var funcAppStorageArgs = new AssignmentStorageAccountArgs(rg.GetName(), resourceNames.FuncAppStorageAccount);
        funcAppStorage = new AssignmentStorageAccount(resourceNames.FuncAppStorageAccount, funcAppStorageArgs.GetStorageAccountArgs());

        var blobContainerArgs = new AssignmentBlobContainerArgs();
        blobContainer = new AssignmentBlobContainer(resourceNames.FuncAppDiagnosticsSettingsContainer, blobContainerArgs);

        var funcAppAspArgs = new AssignmentAppServicePlanArgs(rg.GetName(), resourceNames.FuncAppAspName, "Linux", AspSku.FunctionApp);
        funcAppAsp = new AssignmentAppServicePlan(resourceNames.FuncAppAspName, funcAppAspArgs.GetAspArgs());

        funcAppManagedIdentity = new AssignmentUserAssignedIdentity(baseResourceName, rg);

        var funcAppSettings = new List<NameValuePairArgs>()
        {
            new NameValuePairArgs
            {
                Name = "AzureWebJobsStorage",
                Value = funcAppStorage.GetConnectionString(),
            },
        };
        var funcAppDiagnosticSettings = new WebAppDiagnosticLogsConfiguration("FuncAppDiagnosticsSettings", new WebAppDiagnosticLogsConfigurationArgs
        {
            Name = resourceNames.FuncAppDiagnosticsSettings,
            ResourceGroupName = rg.GetName(),
            DetailedErrorMessages = new EnabledConfigArgs
            {
                Enabled = false,
            },
            FailedRequestsTracing = new EnabledConfigArgs
            {
                Enabled = false,
            },
            ApplicationLogs = new ApplicationLogsConfigArgs
            {
                AzureBlobStorage = new AzureBlobStorageApplicationLogsConfigArgs
                {
                    Level = LogLevel.Information,
                    RetentionInDays = 7,
                    SasUrl = blobContainer
                }
            },
            HttpLogs = ,
            Kind = ""
        });
        var funcAppArgs = new AssignmentWebAppArgs(rg.GetName(), funcAppAsp.GetId(), WebAppKind.FunctionApp, funcAppManagedIdentity.GetId(), funcAppSettings);
        funcApp = new AssignmentWebApp(resourceNames.FuncAppName, funcAppArgs.GetWebAppArgs());

        var appServiceAspArgs = new AssignmentAppServicePlanArgs(rg.GetName(), resourceNames.AppServiceAspName, "Linux", AspSku.AppServiceFree);
        appServiceAsp = new AssignmentAppServicePlan(resourceNames.AppServiceAspName, appServiceAspArgs.GetAspArgs());

        var appServiceManagedIndentityArgs = new AssignmentUserAssignedIdentityArgs(rg.GetName(), resourceNames.AppServiceManagedIdentityName);
        appServiceManagedIdentity = new AssignmentUserAssignedIdentity(resourceNames.AppServiceManagedIdentityName, appServiceManagedIndentityArgs.GetUserAssignedIdentityArgs());

        var appServiceArgs = new AssignmentWebAppArgs(rg.GetName(), appServiceAsp.GetId(), WebAppKind.WebApp, appServiceManagedIdentity.GetId(), new List<Pulumi.AzureNative.Web.Inputs.NameValuePairArgs>());
        appService = new AssignmentWebApp(resourceNames.AppServiceName, appServiceArgs.GetWebAppArgs());
    }
}
