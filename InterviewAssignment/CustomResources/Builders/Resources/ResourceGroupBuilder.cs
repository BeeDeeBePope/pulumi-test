using Pulumi.AzureNative.Resources;

namespace InterviewAssignmnet.CustomResources.Builders.Resources;

public class ResourceGroupBuilder : AzureResourceBuilder<ResourceGroup, ResourceGroupArgs>
{
    private readonly ResourceGroupArgs args = new();

    public ResourceGroupBuilder(string nameSuffix)
        : base(nameSuffix) { }

    public override ResourceGroup Build() => new ResourceGroup(Name, args);

    public override ResourceGroupArgsBuilder InitializeArgs() =>
        new ResourceGroupArgsBuilder(this, args);
}

public class ResourceGroupArgsBuilder : AzureResourceArgsBuilder<ResourceGroup, ResourceGroupArgs>
{
    private readonly ResourceGroupArgs args;

    public ResourceGroupArgsBuilder(ResourceGroupBuilder resourceBuilder, ResourceGroupArgs args)
        : base(resourceBuilder)
    {
        this.args = args;
        this.args.ResourceGroupName = resourceBuilder.Name;
    }

    public ResourceGroupArgsBuilder WithLocation(Pulumi.Input<string> azureLocation)
    {
        args.Location = azureLocation;
        return this;
    }
}
