using InterviewAssignmnet.CustomResources.Builders.ManagedIdentity;
using InterviewAssignmnet.CustomResources.Resources;
using Pulumi.AzureNative.ManagedIdentity;

namespace InterviewAssignmnet.CustomResources.ManagedIdentity;

public class AssignmentUserAssignedIdentity
{
    private readonly UserAssignedIdentity identity;

    public AssignmentUserAssignedIdentity(string nameSuffix, AssignmentResourceGroup rg)
    {
        identity = new UserAssignedIdentityBuilder(nameSuffix)
            .InitializeArgs()
            .WithResourceGroup(rg.Name)
            .WithLocation(rg.Location)
            .Finalize()
            .Build();
    }

    public Pulumi.Output<string> Id => identity.Id;
}
