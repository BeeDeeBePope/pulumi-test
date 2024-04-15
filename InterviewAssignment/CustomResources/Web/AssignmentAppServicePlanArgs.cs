using System;
using System.Collections.Generic;
using Pulumi;
using Pulumi.AzureNative.Web;
using Pulumi.AzureNative.Web.Inputs;

namespace InterviewAssignmnet.CustomResources.Web;

public enum AspSku
{
    FunctionApp,
    AppServiceFree,
    AppServiceStandard
}
public class AssignmentAppServicePlanArgs
{
    private readonly Input<string> rgName;
    private readonly Input<string> aspName;
    private readonly Input<string> aspOs;
    private readonly AspSku aspSku;
    private readonly Dictionary<AspSku, SkuDescriptionArgs> SkuDescriptionArgs = new() {
        {
            AspSku.FunctionApp, new()
            {
                Tier = "Dynamic",
                Name = "Y1"
            }
        },
        {
            AspSku.AppServiceFree, new()
            {
                Tier = "Free",
                Name = "F1"
            }
        }
    };


    public AssignmentAppServicePlanArgs(Input<string> rgName, Input<string> aspName, Input<string> aspOs, AspSku aspSku)
    {
        this.rgName = rgName;
        this.aspName = aspName;
        this.aspOs = aspOs;
        this.aspSku = aspSku;
    }

    public AppServicePlanArgs GetAspArgs() => new()
    {
        ResourceGroupName = rgName,
        Kind = aspOs,
        Reserved = aspOs == (Input<string>)"Linux",
        Name = aspName,
        Sku = SkuDescriptionArgs[aspSku],
    };
}
