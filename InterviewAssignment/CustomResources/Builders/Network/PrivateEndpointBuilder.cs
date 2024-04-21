using Pulumi.AzureNative.Network;

namespace InterviewAssignmnet.CustomResources.Builders.Network;

public class PrivateEndpointBuilder : AzureResourceBuilder<PrivateEndpoint, PrivateEndpointArgs>
{
    private readonly PrivateEndpointArgs args = new();

    public PrivateEndpointBuilder(string nameSuffix)
        : base(nameSuffix) { }

    public override PrivateEndpoint Build() => new PrivateEndpoint(Name, args);

    public override PrivateEndpointArgsBuilder InitializeArgs() =>
        new PrivateEndpointArgsBuilder(this, args);
}

public class PrivateEndpointArgsBuilder
    : AzureResourceArgsBuilder<PrivateEndpoint, PrivateEndpointArgs>
{
    private readonly PrivateEndpointArgs args;

    public PrivateEndpointArgsBuilder(PrivateEndpointBuilder resourceBuilder, PrivateEndpointArgs args)
        : base(resourceBuilder)
    {
        this.args = args;
        this.args.PrivateEndpointName = resourceBuilder.Name;
    }

    public PrivateEndpointArgsBuilder WithLocation(Pulumi.Input<string> azureLocation)
    {
        args.Location = azureLocation;
        return this;
    }

    public PrivateEndpointArgsBuilder WithResourceGroup(Pulumi.Input<string> rgName)
    {
        args.ResourceGroupName = rgName;
        return this;
    }

    public PrivateEndpointArgsBuilder WithSubnet(Pulumi.Input<string> subnetId)
    {
        args.Subnet = new Pulumi.AzureNative.Network.Inputs.SubnetArgs {
            Id = subnetId
        };
        return this;
    }

    public PrivateEndpointArgsBuilder WithPrivateLinkServiceConnection(Pulumi.Input<string> pLinkConnectionId)
    {
        args.PrivateLinkServiceConnections = new [] {new Pulumi.AzureNative.Network.Inputs.PrivateLinkServiceConnectionArgs {
            PrivateLinkServiceId =pLinkConnectionId,
            Name = $"{resourceBuilder.Name}-private-link-connection",
        }}
        ;
        return this;
    }
}
