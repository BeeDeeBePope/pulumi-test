using Pulumi.AzureNative.Web;

namespace InterviewAssignmnet.CustomResources.Builders.Web;

public class WebAppPrivateEndpointConnectionBuilder
    : AzureResourceBuilder<WebAppPrivateEndpointConnection, WebAppPrivateEndpointConnectionArgs>
{
    private readonly WebAppPrivateEndpointConnectionArgs args = new();

    public WebAppPrivateEndpointConnectionBuilder(string nameSuffix)
        : base(nameSuffix) { }

    public override WebAppPrivateEndpointConnection Build() => new WebAppPrivateEndpointConnection(Name, args);

    public override WebAppPrivateEndpointConnectionArgsBuilder InitializeArgs() =>
        new WebAppPrivateEndpointConnectionArgsBuilder(this, args);
}

public class WebAppPrivateEndpointConnectionArgsBuilder
    : AzureResourceArgsBuilder<WebAppPrivateEndpointConnection, WebAppPrivateEndpointConnectionArgs>
{
    private readonly WebAppPrivateEndpointConnectionArgs args;

    public WebAppPrivateEndpointConnectionArgsBuilder(
        WebAppPrivateEndpointConnectionBuilder resourceBuilder,
        WebAppPrivateEndpointConnectionArgs args
    )
        : base(resourceBuilder)
    {
        this.args = args;
        this.args.PrivateEndpointConnectionName = resourceBuilder.Name;
    }

    public WebAppPrivateEndpointConnectionArgsBuilder WithResourceGroup(Pulumi.Input<string> rgName)
    {
        args.ResourceGroupName = rgName;
        return this;
    }

    public WebAppPrivateEndpointConnectionArgsBuilder WithWebApp(Pulumi.Input<string> webAppName)
    {
        args.Name = webAppName;
        return this;
    }
}
