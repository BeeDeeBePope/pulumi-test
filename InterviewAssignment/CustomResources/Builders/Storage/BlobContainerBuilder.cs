using Pulumi.AzureNative.Storage;

namespace InterviewAssignmnet.CustomResources.Builders.Storage;

public class BlobContainerBuilder : AzureResourceBuilder<BlobContainer, BlobContainerArgs>
{
    private readonly BlobContainerArgs args = new();

    public BlobContainerBuilder(string nameSuffix)
        : base(nameSuffix) { }

    public override BlobContainer Build() => new BlobContainer(Name, args);

    public override BlobContainerArgsBuilder InitializeArgs() =>
        new BlobContainerArgsBuilder(this, args);
}

public class BlobContainerArgsBuilder : AzureResourceArgsBuilder<BlobContainer, BlobContainerArgs>
{
    private readonly BlobContainerArgs args;

    public BlobContainerArgsBuilder(BlobContainerBuilder resourceBuilder, BlobContainerArgs args)
        : base(resourceBuilder)
    {
        this.args = args;
        this.args.ContainerName = resourceBuilder.Name;
    }

    public BlobContainerArgsBuilder WithResourceGroup(Pulumi.Input<string> rgName)
    {
        args.ResourceGroupName = rgName;
        return this;
    }

    public BlobContainerArgsBuilder WithStorageAccount(Pulumi.Input<string> storageAccountName)
    {
        args.AccountName = storageAccountName;
        return this;
    }
}
