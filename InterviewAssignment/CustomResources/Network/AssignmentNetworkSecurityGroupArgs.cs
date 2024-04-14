using Pulumi;
using Pulumi.AzureNative.Network;

namespace InterviewAssignmnet.CustomResources.Network;

public class AssignmentNetworkSecurityGroupArgs
{
    private readonly Input<string> rgName;
    private readonly Input<string> nsgName;
    private readonly InputList<Pulumi.AzureNative.Network.Inputs.SecurityRuleArgs> securtiyRules;

    public AssignmentNetworkSecurityGroupArgs(Input<string> rgName, Input<string> nsgName, InputList<Pulumi.AzureNative.Network.Inputs.SecurityRuleArgs> securityRules)
    {
        this.rgName = rgName;
        this.nsgName = nsgName;
        this.securtiyRules = securityRules;
    }

    public NetworkSecurityGroupArgs GetNetworkSecurityGroupArgs() => new()
    {
        ResourceGroupName = rgName,
        NetworkSecurityGroupName = nsgName,
        SecurityRules = securtiyRules,
    };
}