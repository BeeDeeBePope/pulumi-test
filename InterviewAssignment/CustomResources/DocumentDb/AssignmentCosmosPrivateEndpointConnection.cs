using InterviewAssignmnet.CustomResources.Builders.DocumentDB;
using InterviewAssignmnet.CustomResources.Resources;
using Pulumi.AzureNative.DocumentDB;

namespace InterviewAssignmnet.CustomResources.DocumentDB
;

public class AssignmentCosmosPrivateEndpointConnection : IPrivateEndpointConnection
{
    private readonly PrivateEndpointConnection privateEndpoint;

    public AssignmentCosmosPrivateEndpointConnection(string nameSuffix, AssignmentResourceGroup rg, AssignmentDatabaseAccount db)
    {
        privateEndpoint = new PrivateEndpointConnectionBuilder(nameSuffix)
            .InitializeArgs()
            .WithResourceGroup(rg.Name)
            .WithCosmosDb(db.Name)
            .Finalize()
            .Build();
    }

    public Pulumi.Output<string> Id => privateEndpoint.Id;
}
