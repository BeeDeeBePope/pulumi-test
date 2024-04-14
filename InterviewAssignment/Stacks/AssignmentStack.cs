using Pulumi;
using InterviewAssignmnet.CustomResources.Network;
using InterviewAssignmnet.CustomResources.Resources;
using System.Collections.Generic;

namespace InterviewAssignmnet.Stacks;
public class AssignmentStack : Stack
{
    //Vnet
    //snet x2
    //private endpoing
    //function app
    //app service
    //asp x2
    //cosmos db
    //cosmos collection
    //storage account
    //blob container
    //user assigned managed identity

    private readonly AssignmentResourceGroup rg;
    private readonly AssignmentVirtualNetwork vnet;
    private readonly AssignmentNetworkSecurityGroup funcAppSnetNsg;
    private readonly AssignmentNetworkSecurityGroup appServiceSnetNsg;
    private readonly AssignmentSubnet funcAppSubnet;
    private readonly AssignmentSubnet appServiceSubnet;

    public AssignmentStack()
    {
        var config = new Config("general");
        var azureConfig = new Config("azure-native");
        var networkConfig = new Config("network");

        var location = azureConfig.Require("location");

        var vnetAddressSpace = networkConfig.Require("vnetAddressSpace");
        var funcAppSubnetAddressSpace = networkConfig.Require("funcAppSubnetAddressSpace");
        var appServiceSubnetAddressSpace = networkConfig.Require("appServiceSubnetAddressSpace");

        rg = new AssignmentResourceGroup($"assignment-{location}");

        var assignmentVnetArgs = new AssignmentVirtualNetworkArgs(rg.GetName(), rg.GetLocation(), vnetAddressSpace);
        vnet = new AssignmentVirtualNetwork($"assignment-{location}", assignmentVnetArgs.GetVnetArgs());

        var defaultNsgRules = new AssignmentNetworkSecurityRules();
        var defaultNsgArgs = new AssignmentNetworkSecurityGroupArgs(rg.GetName(), $"assignment-{location}-func-app", defaultNsgRules.GetRules());

        funcAppSnetNsg = new AssignmentNetworkSecurityGroup($"assignment-{location}-func-app", defaultNsgArgs.GetNetworkSecurityGroupArgs());

        var funcAppSubnetArgs = new AssignmentSubnetArgs(rg.GetName(), vnet.GetName(), funcAppSubnetAddressSpace);
        funcAppSubnet = new AssignmentSubnet($"assignment-{location}-func-app", funcAppSubnetArgs.GetSubnetArgs());

        var appServiceSubnetArgs = new AssignmentSubnetArgs(rg.GetName(), vnet.GetName(), appServiceSubnetAddressSpace);
        appServiceSubnet = new AssignmentSubnet($"assignment-{location}-app-service", appServiceSubnetArgs.GetSubnetArgs());
    }
}
