using System;
using InterviewAssignmnet.Extensions;
using Pulumi;
using Pulumi.AzureNative.Resources;

namespace InterviewAssignmnet.CustomResources.Resources;

public class AssignmentResourceGroup
{
    private ResourceGroup rg;
    public AssignmentResourceGroup(string name, ResourceGroupArgs? rgArgs = default)
    {
        this.rg = new ResourceGroup(name, rgArgs);
    }

    public Output<string> Name => rg.Name;
    public Output<string> Location => rg.Location;
}
