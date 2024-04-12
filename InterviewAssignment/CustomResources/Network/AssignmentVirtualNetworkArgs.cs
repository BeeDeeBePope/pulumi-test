namespace InterviewAssignmnet.CustomResources.Network;

using Pulumi;
using Pulumi.AzureNative.Network;
using Pulumi.AzureNative.Network.Inputs;

public class AssignmentVirtualNetworkArgs
{
    private readonly VirtualNetworkArgs vnetArgs;
    public AssignmentVirtualNetworkArgs(Input<string> rgName, Input<string> location, Input<string> addressPrefix)
    {
        this.vnetArgs = new VirtualNetworkArgs
        {
            AddressSpace = new AddressSpaceArgs
            {
                AddressPrefixes = new[] { addressPrefix }
            },
            ResourceGroupName = rgName,
            Location = location
        };
    }

    public VirtualNetworkArgs GetVnetArgs()
    {
        return vnetArgs;
    }
}