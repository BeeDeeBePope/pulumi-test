using Pulumi;
using Pulumi.AzureNative.Storage;
using Pulumi.AzureNative.Storage.Inputs;

namespace InterviewAssignmnet.CustomResources.Storage;

public class AssignmentBlobContainer
{
    private BlobContainerArgs args;
    private BlobContainer blobContianer;

    public AssignmentBlobContainer(string name, BlobContainerArgs blobContainerArgs)
    {
        args = blobContainerArgs;
        blobContianer = new BlobContainer(name, blobContainerArgs);
    }

    public Output<string> GetSasUrl()
    {
        var sasToken = Output.Create(ListStorageAccountSAS.InvokeAsync(new ListStorageAccountSASArgs
        {
            AccountName = args.AccountName,
            Protocols = HttpProtocol.Https,
            SharedAccessStartTime = "2023-01-01T00:00:00Z",
            SharedAccessExpiryTime = "2023-12-31T00:00:00Z",
            ResourceGroupName = "resourceGroupName",
            Permissions = Permissions.R + Permissions.W + Permissions.D,
            Services = Services.Blobs,
            ResourceTypes = SignedResourceTypes.Service + SignedResourceTypes.Container + SignedResourceTypes.Object,
        }));
    }
}
