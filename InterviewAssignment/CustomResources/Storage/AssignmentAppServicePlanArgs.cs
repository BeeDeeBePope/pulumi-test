using Pulumi;
using Pulumi.AzureNative.Storage;
using Pulumi.AzureNative.Storage.Inputs;

namespace InterviewAssignmnet.CustomResources.Storage;

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
