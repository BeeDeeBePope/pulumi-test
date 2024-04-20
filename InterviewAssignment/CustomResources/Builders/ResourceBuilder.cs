using System;
using System.Text;
using InterviewAssignmnet.Extensions;
using Pulumi;
using Pulumi.AzureNative.Resources;

namespace InterviewAssignmnet.CustomResources;

public abstract class AzureResourceBuilder<TResource, TResourceArgs>
  where TResource : CustomResource
  where TResourceArgs : Pulumi.ResourceArgs
{
    public string Name { get; private set; }
    protected AzureResourceBuilder(string nameSuffix)
    {
        this.Name = GetResourceName(typeof(TResource), nameSuffix);
    }
    public abstract AzureResourceArgsBuilder<TResource, TResourceArgs> InitializeArgs();
    public abstract TResource Build();

    private string GetResourceName(Type resourceType, string nameSuffix)
    {
        var azureConfig = new Config("azure-native");
        var resourceLocation = azureConfig.Require("location");
        var baseResourceNameBuilder = new StringBuilder($"assignment-")
            .Append(resourceLocation);

        if (nameSuffix.InNullOrEmpty())
        {
            baseResourceNameBuilder = baseResourceNameBuilder
                .Append('-')
                .Append(nameSuffix);
        }

        return baseResourceNameBuilder.ToString().AddPrefixIfRequired<TResource>();
    }
}

public abstract class AzureResourceArgsBuilder<TResource, TResourceArgs>
  where TResourceArgs : Pulumi.ResourceArgs
  where TResource : CustomResource
{
    protected readonly AzureResourceBuilder<TResource, TResourceArgs> resourceBuilder;

    protected AzureResourceArgsBuilder(AzureResourceBuilder<TResource, TResourceArgs> resourceBuilder)
    {
        this.resourceBuilder = resourceBuilder;
    }

    public AzureResourceBuilder<TResource, TResourceArgs> Finalize()
    {
        return resourceBuilder;
    }
}