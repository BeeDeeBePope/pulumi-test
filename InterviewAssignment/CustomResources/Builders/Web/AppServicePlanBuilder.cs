using System.Collections.Generic;
using Pulumi;
using Pulumi.AzureNative.Web;
using Pulumi.AzureNative.Web.Inputs;

namespace InterviewAssignmnet.CustomResources.Builders.Web;

public enum AspSku
{
    FunctionApp,
    FunctionsPremium,
    AppServiceFree,
    AppServiceStandard
}

public class AppServicePlanBuilder : AzureResourceBuilder<AppServicePlan, AppServicePlanArgs>
{
    private readonly AppServicePlanArgs args = new();

    public AppServicePlanBuilder(string nameSuffix)
        : base(nameSuffix) { }

    public override AppServicePlan Build() => new AppServicePlan(Name, args);

    public override AppServicePlanArgsBuilder InitializeArgs() =>
        new AppServicePlanArgsBuilder(this, args);
}

public class AppServicePlanArgsBuilder
    : AzureResourceArgsBuilder<AppServicePlan, AppServicePlanArgs>
{
    private readonly AppServicePlanArgs args;
    private readonly Dictionary<AspSku, SkuDescriptionArgs> SkuDescriptionArgs =
        new()
        {
            {
                AspSku.FunctionApp,
                new() { Tier = "Dynamic", Name = "Y1" }
            },
            {
                AspSku.FunctionsPremium,
                new() { Tier = "ElasticPremium", Name = "EP1" }
            },
            {
                AspSku.AppServiceFree,
                new() { Tier = "Free", Name = "F1" }
            },
            {
                AspSku.AppServiceStandard,
                new() { Tier = "Standard", Name = "S1" }
            }
        };

    public AppServicePlanArgsBuilder(AppServicePlanBuilder resourceBuilder, AppServicePlanArgs args)
        : base(resourceBuilder)
    {
        this.args = args;
        this.args.Name = resourceBuilder.Name;
    }

    public AppServicePlanArgsBuilder WithLocation(Pulumi.Input<string> azureLocation)
    {
        args.Location = azureLocation;
        return this;
    }

    public AppServicePlanArgsBuilder WithResourceGroup(Pulumi.Input<string> rgName)
    {
        args.ResourceGroupName = rgName;
        return this;
    }

    public AppServicePlanArgsBuilder WithOsKind(Pulumi.Input<string> osKind)
    {
        args.Kind = osKind;
        args.Reserved = osKind == (Input<string>)"Linux";
        return this;
    }

    public AppServicePlanArgsBuilder WithSku(AspSku aspSku)
    {
        args.Sku = SkuDescriptionArgs[aspSku];
        return this;
    }
}
