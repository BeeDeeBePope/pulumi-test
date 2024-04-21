using InterviewAssignmnet.CustomResources.Builders.Network;
using Pulumi.AzureNative.Network;

namespace InterviewAssignmnet.CustomResources.Network;

public class AssignmentPrivateEndpoint
{
    private readonly PrivateEndpoint privateEndpoint;

    public AssignmentPrivateEndpoint(
        string nameSuffix,
        Resources.AssignmentResourceGroup rg,
        AssignmentSubnet subnet,
        IPrivateEndpointConnection privateLinkServiceConnection
    )
    {
        privateEndpoint = new PrivateEndpointBuilder(nameSuffix)
            .InitializeArgs()
            .WithResourceGroup(rg.Name)
            .WithLocation(rg.Location)
            .WithSubnet(subnet.Id)
            .WithPrivateLinkServiceConnection(privateLinkServiceConnection.Id)
            .Finalize()
            .Build();
    }

    public Pulumi.Output <string> Id => privateEndpoint.Id;
}
