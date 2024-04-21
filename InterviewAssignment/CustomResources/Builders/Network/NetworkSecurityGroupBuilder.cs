using System;
using System.Collections.Generic;
using Pulumi;
using Pulumi.AzureNative.Network;

namespace InterviewAssignmnet.CustomResources.Builders.Network;

public class NetworkSecurityGroupBuilder
    : AzureResourceBuilder<NetworkSecurityGroup, NetworkSecurityGroupArgs>
{
    private readonly NetworkSecurityGroupArgs args = new();

    public NetworkSecurityGroupBuilder(string nameSuffix)
        : base(nameSuffix) { }

    public override NetworkSecurityGroup Build() => new NetworkSecurityGroup(Name, args);

    public override NetworkSecurityGroupArgsBuilder InitializeArgs() =>
        new NetworkSecurityGroupArgsBuilder(this, args);
}

public class NetworkSecurityGroupArgsBuilder
    : AzureResourceArgsBuilder<NetworkSecurityGroup, NetworkSecurityGroupArgs>
{
    private readonly NetworkSecurityGroupArgs args;
    private readonly List<Pulumi.AzureNative.Network.Inputs.SecurityRuleArgs> securityRules = new();

    public NetworkSecurityGroupArgsBuilder(
        NetworkSecurityGroupBuilder resourceBuilder,
        NetworkSecurityGroupArgs args
    )
        : base(resourceBuilder)
    {
        this.args = args;
        this.args.NetworkSecurityGroupName = resourceBuilder.Name;
    }

    public NetworkSecurityGroupArgsBuilder WithLocation(Input<string> azureLocation)
    {
        args.Location = azureLocation;
        return this;
    }

    public NetworkSecurityGroupArgsBuilder WithResourceGroup(Input<string> rgName)
    {
        args.ResourceGroupName = rgName;
        return this;
    }

    public NetworkSecurityGroupArgsBuilder WithNewSecurityRule(
        Func<SecurityRuleBuilder, Pulumi.AzureNative.Network.Inputs.SecurityRuleArgs> builder
    )
    {
        securityRules.Add(builder(new SecurityRuleBuilder()));
        args.SecurityRules = securityRules;
        return this;
    }
}
