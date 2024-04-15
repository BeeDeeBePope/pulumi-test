namespace InterviewAssignmnet.CustomResources.Network;
using System.Collections.Generic;
using Pulumi.AzureNative.Network;

public class AssignmentNetworkSecurityRules
{
    private readonly List<Pulumi.AzureNative.Network.Inputs.SecurityRuleArgs> rules;
    public AssignmentNetworkSecurityRules()
    {
        rules = new List<Pulumi.AzureNative.Network.Inputs.SecurityRuleArgs>
        {
            new()
            {
                Priority = 100,
                Access = SecurityRuleAccess.Allow,
                Direction = SecurityRuleDirection.Inbound,
                Protocol = SecurityRuleProtocol.Tcp,
                SourcePortRange = "*",
                DestinationPortRange = "*",
                SourceAddressPrefix = "VirtualNetwork",
                DestinationAddressPrefix = "VirtualNetwork",
                Name = "AllowInbloudAllFromVnetToVnet",
            },
            new()
            {
                Priority = 1000,
                Access = SecurityRuleAccess.Deny,
                Direction = SecurityRuleDirection.Inbound,
                Protocol = SecurityRuleProtocol.Tcp,
                SourcePortRange = "*",
                DestinationPortRange = "*",
                SourceAddressPrefix = "Internet",
                DestinationAddressPrefix = "VirtualNetwork",
                Name = "DenyInboudAllFromInternetToVnet",
            }
        };
    }

    public List<Pulumi.AzureNative.Network.Inputs.SecurityRuleArgs> GetRules() => rules;
}

