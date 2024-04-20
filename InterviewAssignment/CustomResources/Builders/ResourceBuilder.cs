using System.Text;
using InterviewAssignmnet.Extensions;
using InterviewAssignmnet.Utility;


namespace InterviewAssignmnet.CustomResources;

public abstract class AzureResourceBuilder<TResource, TResourceArgs>
    where TResource : Pulumi.CustomResource
    where TResourceArgs : Pulumi.ResourceArgs
{
    public string Name { get; private set; }

    protected AzureResourceBuilder(string nameSuffix)
    {
        this.Name = GetResourceName(nameSuffix);
    }

    public abstract AzureResourceArgsBuilder<TResource, TResourceArgs> InitializeArgs();
    public abstract TResource Build();

    private string GetResourceName(string nameSuffix)
    {
        var resourceLocation = Config.Azure.Location;
        var polandShortened = resourceLocation == "polandcentral" ? "pl" : resourceLocation;
        var baseResourceNameBuilder = new StringBuilder($"task-").Append(polandShortened);
        if (!nameSuffix.IsNullOrEmpty())
        {
            baseResourceNameBuilder = baseResourceNameBuilder.Append('-').Append(nameSuffix);
        }
        return baseResourceNameBuilder.ToString().AddPrefixIfRequired<TResource>().ApplySpecialFormattingIfRequired<TResource>();
    }
}

public abstract class AzureResourceArgsBuilder<TResource, TResourceArgs>
    where TResourceArgs : Pulumi.ResourceArgs
    where TResource : Pulumi.CustomResource
{
    protected readonly AzureResourceBuilder<TResource, TResourceArgs> resourceBuilder;

    protected AzureResourceArgsBuilder(
        AzureResourceBuilder<TResource, TResourceArgs> resourceBuilder
    )
    {
        this.resourceBuilder = resourceBuilder;
    }

    public AzureResourceBuilder<TResource, TResourceArgs> Finalize()
    {
        return resourceBuilder;
    }
}
