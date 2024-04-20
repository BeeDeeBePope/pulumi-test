using Pulumi.AzureNative.ManagedIdentity;

namespace InterviewAssignmnet.CustomResources.Builders.ManagedIdentity;

public class UserAssignedIdentityBuilder
    : AzureResourceBuilder<UserAssignedIdentity, UserAssignedIdentityArgs>
{
    private readonly UserAssignedIdentityArgs args = new();

    public UserAssignedIdentityBuilder(string nameSuffix)
        : base(nameSuffix) { }

    public override UserAssignedIdentity Build() => new UserAssignedIdentity(Name, args);

    public override UserAssignedIdentityArgsBuilder InitializeArgs() =>
        new UserAssignedIdentityArgsBuilder(this, args);
}

public class UserAssignedIdentityArgsBuilder
    : AzureResourceArgsBuilder<UserAssignedIdentity, UserAssignedIdentityArgs>
{
    private readonly UserAssignedIdentityArgs args;

    public UserAssignedIdentityArgsBuilder(
        UserAssignedIdentityBuilder resourceBuilder,
        UserAssignedIdentityArgs args
    )
        : base(resourceBuilder)
    {
        this.args = args;
        this.args.ResourceName = resourceBuilder.Name;
    }

    public UserAssignedIdentityArgsBuilder WithLocation(Pulumi.Input<string> azureLocation)
    {
        args.Location = azureLocation;
        return this;
    }

    public UserAssignedIdentityArgsBuilder WithResourceGroup(Pulumi.Input<string> rgName)
    {
        args.ResourceGroupName = rgName;
        return this;
    }
}
