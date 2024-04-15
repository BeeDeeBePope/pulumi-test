namespace InterviewAssignmnet.CustomResources.Network;

using System;
using InterviewAssignmnet.Extensions;
using Pulumi;
using Pulumi.AzureNative.Network;

public class AssignmentVirtualNetwork
{
    private VirtualNetwork vnet;
    public AssignmentVirtualNetwork(string name, VirtualNetworkArgs args)
    {
        this.vnet = new VirtualNetwork(name, args);
    }

    internal Output<string> GetName() => vnet.Name;
}
