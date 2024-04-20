namespace InterviewAssignmnet.CustomResources.Storage;

using InterviewAssignmnet.CustomResources.Builders.Storage;
using InterviewAssignmnet.CustomResources.Resources;
using Pulumi;
using Pulumi.AzureNative.Storage;
using Pulumi.AzureNative.Storage.Inputs;

public class AssignmentStorageAccount
{
    private readonly StorageAccount storageAccount;
    private readonly AssignmentResourceGroup rg;

    public AssignmentStorageAccount(string name, AssignmentResourceGroup rg)
    {
        this.rg = rg;
        storageAccount = new StorageAccountBuilder(name)
            .InitializeArgs()
            .WithLocation(rg.Location)
            .WithResourceGroup(rg.Name)
            .Finalize()
            .Build();
    }

    public Output<string> Id => storageAccount.Id;

    public Input<string> Name => storageAccount.Name;

    public Output<string> GetConnectionString()
    {
        // Retrieve the primary storage account key.
        var storageAccountKeys = ListStorageAccountKeys.Invoke(
            new ListStorageAccountKeysInvokeArgs
            {
                ResourceGroupName = rg.Name,
                AccountName = storageAccount.Name,
            }
        );

        return storageAccountKeys.Apply(keys =>
        {
            var primaryStorageKey = keys.Keys[0].Value;

            // Build the connection string to the storage account.
            return Output.Format(
                $"DefaultEndpointsProtocol=https;AccountName={storageAccount.Name};AccountKey={primaryStorageKey}"
            );
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

    public StorageAccountArgs GetStorageAccountArgs() =>
        new()
        {
            ResourceGroupName = rgName,
            AccountName = accountName,
            AccessTier = AccessTier.Hot,
            Kind = Kind.StorageV2,
            Sku = new SkuArgs { Name = SkuName.Standard_LRS }
        };
}
