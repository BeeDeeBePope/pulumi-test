using InterviewAssignmnet.CustomResources.Builders.Resources;
using Pulumi.AzureNative.Resources;

namespace InterviewAssignmnet.CustomResources.Resources;

public class AssignmentResourceGroup
{
    private readonly ResourceGroup rg;

    public AssignmentResourceGroup(string nameSuffix, string azureLocation)
    {
        this.rg = new ResourceGroupBuilder(nameSuffix)
            .InitializeArgs()
            .WithLocation(azureLocation)
            .Finalize()
            .Build();
    }

    public Pulumi.Output <string> Name => rg.Name;
    public Pulumi.Output <string> Location => rg.Location;
}
