namespace InterviewAssignmnet.CustomResources.Network;

using Pulumi;
using Pulumi.AzureNative.Network;

public class AssignmentVirtualNetwork
{
    private VirtualNetwork vnet;
    public AssignmentVirtualNetwork(string name, VirtualNetworkArgs args)
    {
        this.vnet = new VirtualNetwork(name, args);
    }
}
