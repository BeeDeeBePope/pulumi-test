using InterviewAssignmnet.CustomResources.Builders.Web;
using InterviewAssignmnet.CustomResources.Network;
using InterviewAssignmnet.CustomResources.Resources;
using InterviewAssignmnet.CustomResources.Storage;
using Pulumi.AzureNative.Web;
using Pulumi.AzureNative.Web.Inputs;

namespace InterviewAssignmnet.CustomResources.Web;

public class AssignmentWebApp
{
    private readonly WebApp webApp;

    public AssignmentWebApp(
        string nameSuffix,
        AssignmentResourceGroup rg,
        AssignmentAppServicePlan asp,
        AssignmentStorageAccount storageAccount,
        AssignmentSubnet subnet = default
    )
    {
        this.webApp = new WebAppBuilder(nameSuffix)
            .InitializeArgs()
            .WithLocation(rg.Location)
            .WithResourceGroup(rg.Name)
            .WithAppServicePlan(asp.Id)
            .WithAppKind(WebAppKind.FunctionApp)
            .WithSubnet(subnet.Id)
            .WithSiteConfig(builder =>
                builder
                    .WithNewAppSetting(
                        new NameValuePairArgs
                        {
                            Name = "AzureWebJobsStorage",
                            Value = storageAccount.GetConnectionString()
                        }
                    )
                    .Build()
            )
            .Finalize()
            .Build();
    }
}
