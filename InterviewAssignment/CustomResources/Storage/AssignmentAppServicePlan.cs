using System;
using Pulumi;
using Pulumi.AzureNative.Storage;

namespace InterviewAssignmnet.CustomResources.Storage;

public class AssignmentStorageAccount
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
