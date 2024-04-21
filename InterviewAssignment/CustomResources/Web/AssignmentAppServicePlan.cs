using InterviewAssignmnet.CustomResources.Builders.Web;
using InterviewAssignmnet.CustomResources.Resources;
using Pulumi;
using Pulumi.AzureNative.Web;

namespace InterviewAssignmnet.CustomResources.Web;

public class AssignmentAppServicePlan
{
    private readonly AppServicePlan asp;

    public AssignmentAppServicePlan(string nameSuffix, AssignmentResourceGroup rg, AspSku appSku)
    {
        asp = new AppServicePlanBuilder(nameSuffix)
            .InitializeArgs()
            .WithLocation(rg.Location)
            .WithResourceGroup(rg.Name)
            .WithOsKind("Linux")
            .WithSku(appSku)
            .Finalize()
            .Build();
    }

    public Input<string> Id => asp.Id;
}
