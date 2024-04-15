using System;
using InterviewAssignmnet.Extensions;
using Pulumi;
using Pulumi.AzureNative.Network;

namespace InterviewAssignmnet.CustomResources.Network;

public class AssignmentNetworkSecurityGroup
{
    private NetworkSecurityGroup nsg;
    public AssignmentNetworkSecurityGroup(string name, NetworkSecurityGroupArgs nsgArgs)
    {
        nsg = new NetworkSecurityGroup(name, nsgArgs);
    }

    internal Input<string> GetId() => nsg.Id;
}