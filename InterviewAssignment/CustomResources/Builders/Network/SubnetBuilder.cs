using Pulumi.AzureNative.Network;

namespace InterviewAssignmnet.CustomResources.Builders.Network;

public class SubnetBuilder : AzureResourceBuilder<Subnet, SubnetArgs>
{
    private readonly SubnetArgs args = new();

    public SubnetBuilder(string nameSuffix)
        : base(nameSuffix) { }

    public override Subnet Build() => new Subnet(Name, args);

    public override SubnetArgsBuilder InitializeArgs() => new SubnetArgsBuilder(this, args);
}

public class SubnetArgsBuilder : AzureResourceArgsBuilder<Subnet, SubnetArgs>
{
    private readonly SubnetArgs args;

    public SubnetArgsBuilder(SubnetBuilder resourceBuilder, SubnetArgs args)
        : base(resourceBuilder)
    {
        this.args = args;
        this.args.Name = resourceBuilder.Name;
    }

    public SubnetArgsBuilder WithResourceGroup(Pulumi.Input<string> rgName)
    {
        args.ResourceGroupName = rgName;
        return this;
    }

    public SubnetArgsBuilder WithVirtualNetwork(Pulumi.Input<string> vnetName)
    {
        args.VirtualNetworkName = vnetName;
        return this;
    }

    public SubnetArgsBuilder WithAddressPrefix(Pulumi.Input<string> addressPrefix)
    {
        args.AddressPrefix = addressPrefix;
        return this;
    }

    public SubnetArgsBuilder WithNetworkSecurityGroup(Pulumi.Input<string> nsgId)
    {
        args.NetworkSecurityGroup = new Pulumi.AzureNative.Network.Inputs.NetworkSecurityGroupArgs
        {
            Id = nsgId
        };
        return this;
    }
}
