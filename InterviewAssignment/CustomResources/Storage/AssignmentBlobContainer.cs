using Pulumi;
using Pulumi.AzureNative.Storage;

namespace InterviewAssignmnet.CustomResources.Storage;

public class AssignmentBlobContainer
{
    private readonly BlobContainerArgs args;
    private readonly BlobContainer blobContianer;

    public AssignmentBlobContainer(string name, BlobContainerArgs blobContainerArgs)
    {
        args = blobContainerArgs;
        blobContianer = new BlobContainer(name, blobContainerArgs);
    }

    public Output<string> GetSasUrl()
    {
        var sasToken = Output.Create(ListStorageAccountSAS.InvokeAsync(new ListStorageAccountSASArgs
        {
            AccountName = "",
            Protocols = HttpProtocol.Https,
            SharedAccessStartTime = "2023-01-01T00:00:00Z",
            SharedAccessExpiryTime = "2023-12-31T00:00:00Z",
            ResourceGroupName = "resourceGroupName",
            Permissions = $"{Permissions.R}{Permissions.W}{Permissions.D}",
            Services = Services.B,
            ResourceTypes = SignedResourceTypes.S,
        }));
        return sasToken.Apply(storageSas => storageSas.AccountSasToken);
    }
}
