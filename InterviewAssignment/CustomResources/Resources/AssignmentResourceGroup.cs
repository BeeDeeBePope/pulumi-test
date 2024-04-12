using System;
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

    public Output<string> GetName() => rg.Name;
    public Output<string> GetLocation() => rg.Location;
}