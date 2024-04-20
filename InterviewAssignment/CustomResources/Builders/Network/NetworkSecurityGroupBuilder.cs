using System;
using System.ComponentModel;
using Pulumi;
using Pulumi.AzureNative.Network;

namespace InterviewAssignmnet.CustomResources.Builders.Network;

public class NetworkSecurityGroupBuilder
    : AzureResourceBuilder<NetworkSecurityGroup, NetworkSecurityGroupArgs>
{
    private NetworkSecurityGroupArgs args = new();
    public NetworkSecurityGroupBuilder(string nameSuffix) : base(nameSuffix) { }

    public override NetworkSecurityGroup Build()
        => new NetworkSecurityGroup(Name, args);

    public override NetworkSecurityGroupArgsBuilder InitializeArgs()
        => new NetworkSecurityGroupArgsBuilder(this, args);
}

public class NetworkSecurityGroupArgsBuilder :
    AzureResourceArgsBuilder<NetworkSecurityGroup, NetworkSecurityGroupArgs>
{
    private NetworkSecurityGroupArgs args;
    public NetworkSecurityGroupArgsBuilder(
            NetworkSecurityGroupBuilder resourceBuilder, NetworkSecurityGroupArgs args)
        : base(resourceBuilder)
    {
        this.args = args;
        this.args.NetworkSecurityGroupName = resourceBuilder.Name;
        this.args.SecurityRules = new Pulumi.InputList<Pulumi.AzureNative.Network.Inputs.SecurityRuleArgs>();
    }

    public NetworkSecurityGroupArgsBuilder WithLocation(Pulumi.Input<string> azureLocation)
    {
        args.Location = azureLocation;
        return this;
    }

    public NetworkSecurityGroupArgsBuilder WithResourceGroup(Pulumi.Input<string> rgName)
    {
        args.ResourceGroupName = rgName;
        return this;
    }

    public NetworkSecurityGroupArgsBuilder WithNewSecurityRule(System.Func<SecurityRuleBuilder, SecurityRuleBuilder> builder)
    {
        args.SecurityRules.Add(builder(new SecurityRuleBuilder()).Build());

        new Pulumi.AzureNative.Network.Inputs.SecurityRuleArgs
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
        };
        return this;
    }
}

public class SecurityRuleBuilder
{
    private Pulumi.AzureNative.Network.Inputs.SecurityRuleArgs securityRule;

    public SecurityRuleBuilder()
    {
        this.securityRule = new Pulumi.AzureNative.Network.Inputs.SecurityRuleArgs();
    }

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
        securityRule.SourcePortRange = sourcePortRange.IsT1 ? sourcePortRange.AsT1.ToString() : sourcePortRange.AsT0;
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

    public Pulumi.AzureNative.Network.Inputs.SecurityRuleArgs Build()
    {
        return securityRule;
    }
}
