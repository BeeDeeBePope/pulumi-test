using InterviewAssignmnet.Extensions;
using Pulumi.AzureNative.Network;

namespace InterviewAssignmnet.CustomResources.Network;

public class AssignmentNetworkSecurityGroup
{
    private NetworkSecurityGroup nsg;
    public AssignmentNetworkSecurityGroup(string name, NetworkSecurityGroupArgs nsgArgs)
    {
        nsg = new NetworkSecurityGroup(name.AddPrefixIfRequired<NetworkSecurityGroup>(), nsgArgs);
    }
}