namespace InterviewAssignmnet.CustomResources.Network;

using Pulumi;
using Pulumi.AzureNative.Network;
using Pulumi.AzureNative.Network.Inputs;

public class AssignmentVirtualNetworkArgs
{
    private readonly Input<string> rgName;
    private readonly Input<string> location;
    private readonly Input<string> vnetName;
    private readonly Input<string> addressPrefix;

    public AssignmentVirtualNetworkArgs(Input<string> rgName, Input<string> location, Input<string> vnetName, Input<string> addressPrefix)
    {
        this.rgName = rgName;
        this.location = location;
        this.vnetName = vnetName;
        this.addressPrefix = addressPrefix;
    }

    public VirtualNetworkArgs GetVnetArgs() => new()
    {
        AddressSpace = new AddressSpaceArgs
        {
            AddressPrefixes = new[] { addressPrefix }
        },
        VirtualNetworkName = vnetName,
        ResourceGroupName = rgName,
        Location = location
    };
}