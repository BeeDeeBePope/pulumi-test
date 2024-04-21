using InterviewAssignmnet.CustomResources.Builders.Insights;
using InterviewAssignmnet.CustomResources.Storage;
using InterviewAssignmnet.CustomResources.Web;
using Pulumi.AzureNative.Insights;

namespace InterviewAssignmnet.CustomResources.Insights;

public class AssignmentDiagnosticSetting
{
    private readonly DiagnosticSetting diagnosticSettings;

    public AssignmentDiagnosticSetting(
        string nameSuffix,
        AssignmentStorageAccount storageAccount,
        AssignmentWebApp sourceWebApp
    )
    {
        diagnosticSettings = new DiagnosticSettingBuilder(nameSuffix)
            .InitializeArgs()
            .WithStorageAccount(storageAccount.Id)
            .WithAllLogs()
            .WithAllMetrics()
            .WithWebApp(sourceWebApp)
            .Finalize()
            .Build();
    }
}
