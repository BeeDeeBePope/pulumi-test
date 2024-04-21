using InterviewAssignmnet.CustomResources.Builders.Network;
using InterviewAssignmnet.CustomResources.Resources;
using Pulumi.AzureNative.Network;

namespace InterviewAssignmnet.CustomResources.Network;

public class AssignmentNetworkSecurityGroup
{
    private readonly NetworkSecurityGroup nsg;

    public AssignmentNetworkSecurityGroup(string nameSuffix, AssignmentResourceGroup rg)
    {
        nsg = new NetworkSecurityGroupBuilder(nameSuffix)
            .InitializeArgs()
            .WithLocation(rg.Location)
            .WithResourceGroup(rg.Name)
            .WithNewSecurityRule(ruleBuilder =>
                ruleBuilder
                    .WithPriority(100)
                    .WithAccess(SecurityRuleAccess.Allow)
                    .WithDirection(SecurityRuleDirection.Inbound)
                    .WithProtocol(SecurityRuleProtocol.Tcp)
                    .WithSourcePortRange("*")
                    .WithDestinationPortRange("*")
                    .WithSourceAddressPrefix("VirtualNetwork")
                    .WithDestinationAddressPrefix("VirtualNetwork")
                    .WithName("AllowInbloudAllFromVnetToVnet")
                    .Build()
            )
            .WithNewSecurityRule(ruleBuilder =>
                ruleBuilder
                    .WithPriority(1000)
                    .WithAccess(SecurityRuleAccess.Allow)
                    .WithDirection(SecurityRuleDirection.Inbound)
                    .WithProtocol(SecurityRuleProtocol.Tcp)
                    .WithSourcePortRange("*")
                    .WithDestinationPortRange("*")
                    .WithSourceAddressPrefix("Internet")
                    .WithDestinationAddressPrefix("VirtualNetwork")
                    .WithName("DenyInboudAllFromInternetToVnet")
                    .Build()
            )
            .Finalize()
            .Build();
    }

    public Pulumi.Output <string> Id => nsg.Id;
}
