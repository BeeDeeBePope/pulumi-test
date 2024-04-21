using Pulumi.AzureNative.DocumentDB;

namespace InterviewAssignmnet.CustomResources.Builders.DocumentDB;

public class PrivateEndpointConnectionBuilder
    : AzureResourceBuilder<PrivateEndpointConnection, PrivateEndpointConnectionArgs>
{
    private readonly PrivateEndpointConnectionArgs args = new();

    public PrivateEndpointConnectionBuilder(string nameSuffix)
        : base(nameSuffix) { }

    public override PrivateEndpointConnection Build() => new PrivateEndpointConnection(Name, args);

    public override PrivateEndpointConnectionArgsBuilder InitializeArgs() =>
        new PrivateEndpointConnectionArgsBuilder(this, args);
}

public class PrivateEndpointConnectionArgsBuilder
    : AzureResourceArgsBuilder<PrivateEndpointConnection, PrivateEndpointConnectionArgs>
{
    private readonly PrivateEndpointConnectionArgs args;

    public PrivateEndpointConnectionArgsBuilder(
        PrivateEndpointConnectionBuilder resourceBuilder,
        PrivateEndpointConnectionArgs args
    )
        : base(resourceBuilder)
    {
        this.args = args;
        this.args.PrivateEndpointConnectionName = resourceBuilder.Name;
    }

    public PrivateEndpointConnectionArgsBuilder WithResourceGroup(Pulumi.Input<string> rgName)
    {
        args.ResourceGroupName = rgName;
        return this;
    }

    public PrivateEndpointConnectionArgsBuilder WithCosmosDb(Pulumi.Input<string> dbName)
    {
        args.AccountName = dbName;
        return this;
    }
}
