using Pulumi.AzureNative.Network;

namespace InterviewAssignmnet.CustomResources.Builders.Network;

public class VirtualNetworkBuilder
    : AzureResourceBuilder<VirtualNetwork, VirtualNetworkArgs>
{
    private VirtualNetworkArgs args = new();
    public VirtualNetworkBuilder(string nameSuffix) : base(nameSuffix) { }

    public override VirtualNetwork Build()
        => new VirtualNetwork(Name, args);

    public override VirtualNetworkArgsBuilder InitializeArgs()
        => new VirtualNetworkArgsBuilder(this, args);
}

public class VirtualNetworkArgsBuilder :
    AzureResourceArgsBuilder<VirtualNetwork, VirtualNetworkArgs>
{
    private VirtualNetworkArgs args;
    public VirtualNetworkArgsBuilder(
            VirtualNetworkBuilder resourceBuilder, VirtualNetworkArgs args)
        : base(resourceBuilder)
    {
        this.args = args;
        this.args.VirtualNetworkName = resourceBuilder.Name;
    }

    public VirtualNetworkArgsBuilder WithLocation(Pulumi.Input<string> azureLocation)
    {
        args.Location = azureLocation;
        return this;
    }

    public VirtualNetworkArgsBuilder WithResourceGroup(Pulumi.Input<string> rgName)
    {
        args.ResourceGroupName = rgName;
        return this;
    }

    public VirtualNetworkArgsBuilder WithAddressSpace(Pulumi.Input<string> addressSpace)
    {
        args.AddressSpace = new Pulumi.AzureNative.Network.Inputs.AddressSpaceArgs
        {
            AddressPrefixes = new[] { addressSpace },
        };
        return this;
    }
}