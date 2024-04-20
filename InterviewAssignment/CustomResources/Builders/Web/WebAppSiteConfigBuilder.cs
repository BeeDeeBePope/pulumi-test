using System.Collections.Generic;
using Pulumi.AzureNative.Web.Inputs;

namespace InterviewAssignmnet.CustomResources.Builders.Web;

public class WebAppSiteConfigBuilder
{
    private SiteConfigArgs siteConfig =
        new() { AppSettings = new Pulumi.InputList<NameValuePairArgs>() };

    public WebAppSiteConfigBuilder WithNewAppSetting(Pulumi.Input<NameValuePairArgs> appSetting)
    {
        siteConfig.AppSettings.Concat(appSetting);
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
