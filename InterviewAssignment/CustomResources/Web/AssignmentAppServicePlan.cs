using System;
using Pulumi;
using Pulumi.AzureNative.Web;

namespace InterviewAssignmnet.CustomResources.Web;

public class AssignmentAppServicePlan
{
    private AppServicePlan asp;
    public AssignmentAppServicePlan(string name, AppServicePlanArgs aspArgs)
    {
        asp = new AppServicePlan(name, aspArgs);
    }

    internal Input<string> GetId() => asp.Id;
}
