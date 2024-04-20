using Pulumi.AzureNative.Network;

namespace InterviewAssignmnet.CustomResources.Builders.Network;

public class SecurityRuleBuilder
{
    private readonly Pulumi.AzureNative.Network.Inputs.SecurityRuleArgs securityRule = new();

    public SecurityRuleBuilder WithPriority(int priority)
    {
        securityRule.Priority = priority;
        return this;
    }

    public SecurityRuleBuilder WithAccess(SecurityRuleAccess access)
    {
        securityRule.Access = access;
        return this;
    }

    public SecurityRuleBuilder WithDirection(SecurityRuleDirection direction)
    {
        securityRule.Direction = direction;
        return this;
    }

    public SecurityRuleBuilder WithProtocol(SecurityRuleProtocol protocol)
    {
        securityRule.Protocol = protocol;
        return this;
    }

    public SecurityRuleBuilder WithSourcePortRange(string sourcePortRange)
    {
        securityRule.SourcePortRange = sourcePortRange;
        return this;
    }

    public SecurityRuleBuilder WithDestinationPortRange(string destinationPortRange)
    {
        securityRule.DestinationPortRange = destinationPortRange;
        return this;
    }

    public SecurityRuleBuilder WithSourceAddressPrefix(string sourceAddressPrefix)
    {
        securityRule.SourceAddressPrefix = sourceAddressPrefix;
        return this;
    }

    public SecurityRuleBuilder WithDestinationAddressPrefix(string destinationAddressPrefix)
    {
        securityRule.DestinationAddressPrefix = destinationAddressPrefix;
        return this;
    }

    public SecurityRuleBuilder WithName(string Name)
    {
        securityRule.Name = Name;
        return this;
    }

    public Pulumi.AzureNative.Network.Inputs.SecurityRuleArgs Build()
    {
        return securityRule;
    }
}
