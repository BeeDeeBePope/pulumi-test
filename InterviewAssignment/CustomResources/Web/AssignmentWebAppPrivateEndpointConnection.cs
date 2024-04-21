using InterviewAssignmnet.CustomResources.Builders.Web;
using InterviewAssignmnet.CustomResources.Resources;
using Pulumi.AzureNative.Web;

namespace InterviewAssignmnet.CustomResources.Web
;

public class AssignmentWebAppPrivateEndpointConnection: IPrivateEndpointConnection
{
    private readonly WebAppPrivateEndpointConnection privateEndpoint;

    public AssignmentWebAppPrivateEndpointConnection(string nameSuffix, AssignmentResourceGroup rg, AssignmentWebApp webApp)
    {
        privateEndpoint = new WebAppPrivateEndpointConnectionBuilder(nameSuffix)
            .InitializeArgs()
            .WithResourceGroup(rg.Name)
            .WithWebApp(webApp.Name)
            .Finalize()
            .Build();
    }

    public Pulumi.Output<string> Id => privateEndpoint.Id;
}
