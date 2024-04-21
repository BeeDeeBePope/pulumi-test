using System.Collections.Generic;
using Pulumi.AzureNative.Web.Inputs;

namespace InterviewAssignmnet.CustomResources.Builders.Web;

public class WebAppSiteConfigBuilder
{
    private readonly SiteConfigArgs siteConfig = new();
    private readonly List<NameValuePairArgs> appSettings = new();

    public WebAppSiteConfigBuilder WithNewAppSetting(NameValuePairArgs appSetting)
    {
        appSettings.Add(appSetting);
        siteConfig.AppSettings = appSettings;
        return this;
    }

    public WebAppSiteConfigBuilder WithVirtualNetwork(Pulumi.Input<string> vnetName)
    {
        siteConfig.VnetName = vnetName;
        return this;
    }

    public SiteConfigArgs Build()
    {
        return siteConfig;
    }
}
