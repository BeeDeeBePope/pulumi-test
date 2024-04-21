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
        WebAppKind appKind,
        AssignmentStorageAccount storageAccount,
        AssignmentSubnet? subnet = default
    )
    {
        var builder = new WebAppBuilder(nameSuffix)
            .InitializeArgs()
            .WithLocation(rg.Location)
            .WithResourceGroup(rg.Name)
            .WithAppServicePlan(asp.Id)
            .WithAppKind(appKind);

        if (subnet != null)
        {
            builder = builder.WithSubnet(subnet.Id);
        }

        this.webApp = builder
            .WithSiteConfig(builder =>
                builder
                    .WithNewAppSetting(
                        new NameValuePairArgs
                        {
                            Name = "AzureWebJobsStorage",
                            Value = storageAccount.GetConnectionString()
                        }
                    )
                    .WithNewAppSetting(
                        new NameValuePairArgs { Name = "FUNCTIONS_EXTENSION_VERSION", Value = "~4" }
                    )
                    .Build()
            )
            .Finalize()
            .Build();
    }

    public Pulumi.Output <string> Id => webApp.Id;

    public Pulumi.Output <string> Name => webApp.Name;
}
