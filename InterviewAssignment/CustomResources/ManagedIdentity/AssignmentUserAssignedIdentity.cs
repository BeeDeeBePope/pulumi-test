using Pulumi;
using Pulumi.AzureNative.ManagedIdentity;

namespace InterviewAssignmnet.CustomResources.ManagedIdentity;
public class AssignmentUserAssignedIdentity
{
    private UserAssignedIdentity identity;
    public AssignmentUserAssignedIdentity(string name, UserAssignedIdentityArgs identityArgs)
    {
        identity = new UserAssignedIdentity(name, identityArgs);
    }

    public Output<string> GetId() => identity.Id;
}