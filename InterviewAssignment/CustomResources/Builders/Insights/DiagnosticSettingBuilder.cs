using InterviewAssignmnet.CustomResources.Web;
using Pulumi.AzureNative.Insights;
using Pulumi.AzureNative.Insights.Inputs;

namespace InterviewAssignmnet.CustomResources.Builders.ManagedIdentity;

public class DiagnosticSettingBuilder
    : AzureResourceBuilder<DiagnosticSetting, DiagnosticSettingArgs>
{
    private readonly DiagnosticSettingArgs args = new();

    public DiagnosticSettingBuilder(string nameSuffix)
        : base(nameSuffix) { }

    public override DiagnosticSetting Build() => new DiagnosticSetting(Name, args);

    public override DiagnosticSettingArgsBuilder InitializeArgs() =>
        new DiagnosticSettingArgsBuilder(this, args);
}

public class DiagnosticSettingArgsBuilder
    : AzureResourceArgsBuilder<DiagnosticSetting, DiagnosticSettingArgs>
{
    private readonly DiagnosticSettingArgs args;

    public DiagnosticSettingArgsBuilder(
        DiagnosticSettingBuilder resourceBuilder,
        DiagnosticSettingArgs args
    )
        : base(resourceBuilder)
    {
        this.args = args;
        this.args.Name = resourceBuilder.Name;
    }

    public DiagnosticSettingArgsBuilder WithStorageAccount(Pulumi.Input<string> storageAccountId)
    {
        args.StorageAccountId = storageAccountId;
        return this;
    }

    public DiagnosticSettingArgsBuilder WithAllLogs()
    {
        args.Logs = new[]
        {
            new LogSettingsArgs
            {
                CategoryGroup = "allLogs",
                Enabled = true,
                RetentionPolicy = new RetentionPolicyArgs { Days = 7, Enabled = true }
            }
        };
        return this;
    }

    public DiagnosticSettingArgsBuilder WithAllMetrics()
    {
        args.Metrics = new[]
        {
            new MetricSettingsArgs{
                Category = "AllMetrics",
                Enabled = true,
                RetentionPolicy = new RetentionPolicyArgs
                {
                    Days = 0,
                    Enabled = false,
                },
            }
        };
        return this;
    }

    public DiagnosticSettingArgsBuilder WithWebApp(AssignmentWebApp webApp)
    {
        args.ResourceUri = webApp.Id;
        return this;
    }
}
