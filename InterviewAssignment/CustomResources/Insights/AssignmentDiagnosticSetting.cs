using InterviewAssignmnet.CustomResources.Builders.ManagedIdentity;
using InterviewAssignmnet.CustomResources.Storage;
using InterviewAssignmnet.CustomResources.Web;
using Pulumi.AzureNative.Insights;

namespace InterviewAssignmnet.CustomResources.Insights;

public class AssignmentDiagnosticSetting
{
    private readonly DiagnosticSetting identity;

    public AssignmentDiagnosticSetting(
        string nameSuffix,
        AssignmentStorageAccount storageAccount,
        AssignmentWebApp sourceWebApp
    )
    {
        identity = new DiagnosticSettingBuilder(nameSuffix)
            .InitializeArgs()
            .WithStorageAccount(storageAccount.Id)
            .WithAllLogs()
            .WithAllMetrics()
            .WithWebApp(sourceWebApp)
            .Finalize()
            .Build();
    }

    public Pulumi.Output<string> Id => identity.Id;
    public Pulumi.Output<string> Name => identity.Name;
    // public Pulumi.Output<string> PrincipalId => identity.PrincipalId;
    // public Pulumi.Output<string> ClientId => identity.ClientId;
}
