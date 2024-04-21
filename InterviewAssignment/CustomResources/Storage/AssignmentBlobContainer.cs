using InterviewAssignmnet.CustomResources.Builders.Resources;
using InterviewAssignmnet.CustomResources.Builders.Storage;
using InterviewAssignmnet.CustomResources.Resources;
using Pulumi;
using Pulumi.AzureNative.Storage;

namespace InterviewAssignmnet.CustomResources.Storage;

public class AssignmentBlobContainer
{
    private readonly BlobContainer blobContianer;
    private readonly string rgName;
    private readonly string storageAccountName;

    public AssignmentBlobContainer(
        string nameSuffix,
        AssignmentResourceGroup rg,
        AssignmentStorageAccount storageAccount
    )
    {
        storageAccountName = new StorageAccountBuilder(nameSuffix).Name;
        rgName = new ResourceGroupBuilder("main").Name;
        blobContianer = new BlobContainerBuilder(nameSuffix)
            .InitializeArgs()
            .WithResourceGroup(rg.Name)
            .WithStorageAccount(storageAccount.Name)
            .Finalize()
            .Build();
    }

    public Output<string> GetSasUrl()
    {
        var sasToken = Output.Create(
            ListStorageAccountSAS.InvokeAsync(
                new ListStorageAccountSASArgs
                {
                    AccountName = storageAccountName,
                    Protocols = HttpProtocol.Https,
                    SharedAccessStartTime = System.DateTime.UtcNow.ToString("O"),
                    SharedAccessExpiryTime = System.DateTime.UtcNow.AddDays(1d).ToString("O"),
                    ResourceGroupName = rgName,
                    Permissions = Permissions.W,
                    Services = Services.B,
                    ResourceTypes = SignedResourceTypes.S,
                }
            )
        );
        return sasToken.Apply(storageSas => storageSas.AccountSasToken);
    }
}
