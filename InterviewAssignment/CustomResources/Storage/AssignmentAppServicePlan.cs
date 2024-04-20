namespace InterviewAssignmnet.CustomResources.Storage;

using Pulumi;
using Pulumi.AzureNative.Storage;
using Pulumi.AzureNative.Storage.Inputs;

public class AssignmentStorageAccount : Pulumi.CustomResource
{
    private StorageAccount storageAccount;
    private StorageAccountArgs args;
    public AssignmentStorageAccount(string name, StorageAccountArgs storageAccountArgs)
    {
        args = storageAccountArgs;
        storageAccount = new StorageAccount(name, storageAccountArgs);
    }

    public Output<string> GetId() => storageAccount.Id;
    public Input<string> GetName() => storageAccount.Name;

    public Output<string> GetConnectionString()
    {
        // Retrieve the primary storage account key.
        var storageAccountKeys = ListStorageAccountKeys.Invoke(new ListStorageAccountKeysInvokeArgs
        {
            ResourceGroupName = args.ResourceGroupName,
            AccountName = storageAccount.Name,
        });

        return storageAccountKeys.Apply(keys =>
        {
            var primaryStorageKey = keys.Keys[0].Value;

            // Build the connection string to the storage account.
            return Output.Format($"DefaultEndpointsProtocol=https;AccountName={storageAccount.Name};AccountKey={primaryStorageKey}");
        });
    }

}

public class AssignmentStorageAccountArgs
{
    private readonly Input<string> rgName;
    private readonly Input<string> accountName;

    public AssignmentStorageAccountArgs(Input<string> rgName, Input<string> accountName)
    {
        this.rgName = rgName;
        this.accountName = accountName;
    }

    public StorageAccountArgs GetStorageAccountArgs() => new()
    {
        ResourceGroupName = rgName,
        AccountName = accountName,
        AccessTier = AccessTier.Hot,
        Kind = Kind.StorageV2,
        Sku = new SkuArgs
        {
            Name = SkuName.Standard_LRS
        }
    };
}
