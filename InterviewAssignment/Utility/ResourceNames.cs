using InterviewAssignmnet.Extensions;
using Pulumi.AzureNative.ManagedIdentity;
using Pulumi.AzureNative.Resources;
using Pulumi.AzureNative.Network;
using Pulumi.AzureNative.Storage;
using Pulumi.AzureNative.Web;
using Pulumi;

public class ResourceNames
{
    public ResourceNames(string resourceLocation)
    {
        string baseResourceName = $"assignment-{resourceLocation}";

        RgName = baseResourceName.AddPrefixIfRequired<ResourceGroup>();

        VnetName = baseResourceName.AddPrefixIfRequired<VirtualNetwork>();
        FuncAppSnetName = $"{baseResourceName}-func-app".AddPrefixIfRequired<Subnet>();
        AppServiceSnetName = $"{baseResourceName}-app-service".AddPrefixIfRequired<Subnet>();

        FuncAppNsgName = $"{baseResourceName}-func-app".AddPrefixIfRequired<NetworkSecurityGroup>();
        AppServiceNsgName = $"{baseResourceName}-app-service".AddPrefixIfRequired<NetworkSecurityGroup>();

        FuncAppDiagnosticsSettings = "FuncAppDiagnosticsSettings";
        FuncAppStorageAccount = $"{baseResourceName}funcapp".AddPrefixIfRequired<StorageAccount>();
        FuncAppDiagnosticsSettingsContainer = "FuncAppDiagnosticSettings";
        FuncAppAspName = $"{baseResourceName}-func-app".AddPrefixIfRequired<AppServicePlan>();
        FuncAppName = $"{baseResourceName}-func-app".AddPrefixIfRequired<WebApp>();
        FuncAppManagedIdentityName = $"{baseResourceName}-func-app".AddPrefixIfRequired<UserAssignedIdentity>();

        AppServiceAspName = $"{baseResourceName}-app-service".AddPrefixIfRequired<AppServicePlan>();
        AppServiceName = $"{baseResourceName}-app-service".AddPrefixIfRequired<WebApp>();
        AppServiceManagedIdentityName = $"{baseResourceName}-app-service".AddPrefixIfRequired<UserAssignedIdentity>();
    }
    public string RgName { get; private set; }
    public string VnetName { get; private set; }
    public string FuncAppNsgName { get; private set; }
    public string AppServiceNsgName { get; private set; }
    public string FuncAppSnetName { get; private set; }
    public string AppServiceSnetName { get; private set; }
    public string FuncAppAspName { get; private set; }
    public string FuncAppName { get; private set; }
    public string FuncAppManagedIdentityName { get; private set; }
    public string AppServiceAspName { get; private set; }
    public string AppServiceName { get; private set; }
    public string AppServiceManagedIdentityName { get; private set; }
    public string FuncAppStorageAccount { get; private set; }
    public string FuncAppDiagnosticsSettings { get; private set; }
    public string FuncAppDiagnosticsSettingsContainer { get; private set; }
}