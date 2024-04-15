using System;
using Pulumi;
using Pulumi.AzureNative.Network;

namespace InterviewAssignmnet.CustomResources.Network;

public class AssignmentSubnetArgs
{
    private readonly Input<string> rgName;
    private readonly Input<string> vnetName;
    private readonly Input<string> addressPrefix;
    private readonly Input<string> subnetName;

    public AssignmentSubnetArgs(Input<string> rgName, Input<string> vnetName, Input<string> subnetName, Input<string> addressPrefix)
    {
        this.rgName = rgName;
        this.vnetName = vnetName;
        this.addressPrefix = addressPrefix;
        this.subnetName = subnetName;
    }

    public SubnetArgs GetSubnetArgs() => new()
    {
        ResourceGroupName = rgName,
        VirtualNetworkName = vnetName,
        AddressPrefix = addressPrefix,
        Name = subnetName,
    };
}