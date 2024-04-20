namespace InterviewAssignmnet.CustomResources.Network;

using InterviewAssignmnet.CustomResources.Builders.Network;
using InterviewAssignmnet.CustomResources.Resources;
using InterviewAssignmnet.Utility;
using Pulumi.AzureNative.Network;

public class AssignmentVirtualNetwork
{
    private readonly VirtualNetwork vnet;
    public AssignmentVirtualNetwork(string nameSuffix, AssignmentResourceGroup rg)
    {
        vnet = new VirtualNetworkBuilder(nameSuffix)
            .InitializeArgs()
            .WithLocation(rg.Location)
            .WithResourceGroup(rg.Name)
            .WithAddressSpace(Config.Network.VnetAddressSpace)
            .Finalize()
            .Build();
    }

    internal Pulumi.Output<string> GetName() => vnet.Name;
}
