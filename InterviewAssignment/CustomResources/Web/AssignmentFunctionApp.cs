using Pulumi.AzureNative.Web;

namespace InterviewAssignmnet.CustomResources.Web;

public class AssignmentWebApp {
    private WebApp webApp;
    public AssignmentWebApp(string name, WebAppArgs webAppArgs)
    {
        this.webApp = new WebApp(name, webAppArgs);
    }
}
