using System;
using System.Collections.Generic;
using InterviewAssignmnet.CustomResources.Storage;
using Pulumi.AzureNative.Web;
using Pulumi.AzureNative.Web.Inputs;

namespace InterviewAssignmnet.CustomResources.Builders.Web;

public enum WebAppKind
{
    FunctionApp,
    WebApp
}

public class WebAppBuilder : AzureResourceBuilder<WebApp, WebAppArgs>
{
    private readonly WebAppArgs args = new();

    public WebAppBuilder(string nameSuffix)
        : base(nameSuffix) { }

    public override WebApp Build() => new WebApp(Name, args);

    public override WebAppArgsBuilder InitializeArgs() => new WebAppArgsBuilder(this, args);
}

public class WebAppArgsBuilder : AzureResourceArgsBuilder<WebApp, WebAppArgs>
{
    private readonly WebAppArgs args;
    private readonly Dictionary<WebAppKind, string> appKindMap =
        new() { { WebAppKind.FunctionApp, "FunctionApp" }, { WebAppKind.WebApp, "app" }, };

    public WebAppArgsBuilder(WebAppBuilder resourceBuilder, WebAppArgs args)
        : base(resourceBuilder)
    {
        this.args = args;
        this.args.Name = resourceBuilder.Name;
    }

    public WebAppArgsBuilder WithLocation(Pulumi.Input<string> azureLocation)
    {
        args.Location = azureLocation;
        return this;
    }

    public WebAppArgsBuilder WithResourceGroup(Pulumi.Input<string> rgName)
    {
        args.ResourceGroupName = rgName;
        return this;
    }

    public WebAppArgsBuilder WithAppServicePlan(Pulumi.Input<string> aspId)
    {
        args.ServerFarmId = aspId;
        return this;
    }

    public WebAppArgsBuilder WithAppKind(WebAppKind appKind)
    {
        args.Kind = appKindMap[appKind];
        return this;
    }

    public WebAppArgsBuilder WithSiteConfig(
        System.Func<WebAppSiteConfigBuilder, SiteConfigArgs> build
    )
    {
        args.SiteConfig = build(new WebAppSiteConfigBuilder());
        return this;
    }

    public WebAppArgsBuilder WithUserAssignedIdentity(Pulumi.Input<string> managedIdentityId)
    {
        args.Identity = new ManagedServiceIdentityArgs
        {
            Type = ManagedServiceIdentityType.UserAssigned,
            UserAssignedIdentities = new[] { managedIdentityId }
        };
        return this;
    }

    public WebAppArgsBuilder WithSubnet(Pulumi.Input<string> subnetId)
    {
        args.VirtualNetworkSubnetId = subnetId;
        return this;
    }

    public WebAppArgsBuilder WithDiagnosticsSetting(AssignmentBlobContainer blob)
    {

        var funcAppDiagnosticSettings = new WebAppDiagnosticLogsConfiguration($"{resourceBuilder.Name}-diagnostics", new WebAppDiagnosticLogsConfigurationArgs
        {
            Name = resourceBuilder.Name,
            ResourceGroupName = args.ResourceGroupName,
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
                    // SasUrl = blob.GetSasUrl(),
                }
            },
            HttpLogs = new HttpLogsConfigArgs {
                AzureBlobStorage = new AzureBlobStorageHttpLogsConfigArgs {
                    Enabled = false,
                }
            },
            // Kind = ""
        });

        return this;
    }
}
