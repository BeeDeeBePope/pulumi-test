using Pulumi;
using InterviewAssignmnet.CustomResources.Network;
using InterviewAssignmnet.CustomResources.Resources;

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

    private AssignmentResourceGroup rg;
    private AssignmentVirtualNetwork vnet;

    public AssignmentStack()
    {
        var config = new Config("general");
        var azureConfig = new Config("azure-native");
        var networkConfig = new Config("network");

        var location = azureConfig.Require("location");

        rg = new AssignmentResourceGroup("mainResourceGroup");

        var assignmentVnetArgs = new AssignmentVirtualNetworkArgs(rg.GetName(), rg.GetLocation(), networkConfig.Require("vnetAddressSpace"));
        vnet = new AssignmentVirtualNetwork("mainVnet", assignmentVnetArgs.GetVnetArgs());
    }
}
