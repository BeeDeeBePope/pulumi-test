using System;
using System.Collections.Generic;
using Pulumi;
using Pulumi.AzureNative.Web;
using Pulumi.AzureNative.Web.Inputs;

namespace InterviewAssignmnet.CustomResources.Web;

public class AssignmentWebAppArgs
{
    private readonly Input<string> rgName;
    private readonly Input<string> aspId;
    private readonly List<NameValuePairArgs> appSettings;
    private readonly WebAppKind appKind;
    private readonly Input<string> userAssignedIdentity;
    private readonly Dictionary<WebAppKind, string> appKindMap = new() {
        { WebAppKind.FunctionApp, "FunctionApp" },
        { WebAppKind.WebApp, "app" },
    };

    public AssignmentWebAppArgs(Input<string> rgName, Input<string> aspId, WebAppKind appKind, Input<string> userAssignedIdentity, List<NameValuePairArgs> appSettings)
    {
        this.rgName = rgName;
        this.aspId = aspId;
        this.appSettings = appSettings;
        this.appKind = appKind;
        this.userAssignedIdentity = userAssignedIdentity;
    }
    internal WebAppArgs GetWebAppArgs() => new()
    {
        Kind = appKindMap[appKind],
        ResourceGroupName = rgName,
        ServerFarmId = aspId,
        SiteConfig = new SiteConfigArgs
        {
            AppSettings = appSettings,
        },
        Identity = new ManagedServiceIdentityArgs
        {
            Type = ManagedServiceIdentityType.UserAssigned,
            UserAssignedIdentities = new[] { userAssignedIdentity },
        },
    };
}

public enum WebAppKind
{
    FunctionApp,
    WebApp
}