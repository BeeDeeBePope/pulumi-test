using Pulumi;
using Pulumi.AzureNative.Storage;

namespace InterviewAssignmnet.CustomResources.Storage;

public class AssignmentBlobContainerArgs
{
    private readonly Input<string> rgName;
    private readonly Input<string> storageAccountName;
    private readonly Input<string> containerName;

    public AssignmentBlobContainerArgs(Input<string> rgName, Input<string> storageAccountName, Input<string> containerName)
    {
        this.rgName = rgName;
        this.storageAccountName = storageAccountName;
        this.containerName = containerName;
    }

    public BlobContainerArgs GetBlobContainerArgs() => new()
    {
        AccountName = storageAccountName,
        ContainerName = containerName,
        ResourceGroupName = rgName,
    };
}