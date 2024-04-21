namespace InterviewAssignmnet.CustomResources.Storage;

using InterviewAssignmnet.CustomResources.Builders.Storage;
using InterviewAssignmnet.CustomResources.Resources;
using Pulumi;
using Pulumi.AzureNative.Storage;

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

    public Pulumi.Output <string> Id => storageAccount.Id;

    public Pulumi.Output <string> Name => storageAccount.Name;

    public Pulumi.Output <string> GetConnectionString()
    {
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

            return Output.Format(
                $"DefaultEndpointsProtocol=https;AccountName={storageAccount.Name};AccountKey={primaryStorageKey}"
            );
        });
    }
}
