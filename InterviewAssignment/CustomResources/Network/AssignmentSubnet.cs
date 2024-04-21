using InterviewAssignmnet.CustomResources.Builders.Network;
using InterviewAssignmnet.Utility;
using Pulumi;
using Pulumi.AzureNative.Network;

namespace InterviewAssignmnet.CustomResources.Network;

public class AssignmentSubnet
{
    private readonly Subnet subnet;

    public AssignmentSubnet(
        string nameSuffix,
        Resources.AssignmentResourceGroup rg,
        AssignmentVirtualNetwork vnet,
        AssignmentNetworkSecurityGroup funcAppSnetNsg,
        string addressPrefix
    )
    {
        subnet = new SubnetBuilder(nameSuffix)
            .InitializeArgs()
            .WithResourceGroup(rg.Name)
            .WithVirtualNetwork(vnet.Name)
            .WithNetworkSecurityGroup(funcAppSnetNsg.Id)
            .WithAddressPrefix(addressPrefix)
            .WithWebAppDelegation()
            .Finalize()
            .Build();
    }

    public Output<string> Id => subnet.Id;
}
