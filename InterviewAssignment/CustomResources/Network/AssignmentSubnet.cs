using InterviewAssignmnet.Extensions;
using Pulumi.AzureNative.Network;

namespace InterviewAssignmnet.CustomResources.Network;

public class AssignmentSubnet
{
    private Subnet subnet;
    public AssignmentSubnet(string name, SubnetArgs subnetArgs)
    {
        this.subnet = new Subnet(name.AddPrefixIfRequired<Subnet>(), subnetArgs);
    }
}
