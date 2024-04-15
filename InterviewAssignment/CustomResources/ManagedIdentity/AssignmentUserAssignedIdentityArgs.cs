namespace InterviewAssignmnet.CustomResources.ManagedIdentity;

using Pulumi;
using Pulumi.AzureNative.ManagedIdentity;

public class AssignmentUserAssignedIdentityArgs
{
    private readonly Input<string> rgName;
    private readonly Input<string> identityName;
    public AssignmentUserAssignedIdentityArgs(Input<string> rgName, Input<string> identityName)
    {
        this.rgName = rgName;
        this.identityName = identityName;
    }

    public UserAssignedIdentityArgs GetUserAssignedIdentityArgs() => new()
    {
        ResourceGroupName = rgName,
        ResourceName = identityName,
    };
}
