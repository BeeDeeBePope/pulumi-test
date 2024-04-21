using System.Collections.Generic;
using InterviewAssignmnet.CustomResources.Builders.DocumentDB;
using InterviewAssignmnet.CustomResources.Resources;
using Pulumi.AzureNative.DocumentDB;

namespace InterviewAssignmnet.CustomResources.DocumentDB
;

public class AssignmentDatabaseAccount
{
    private readonly DatabaseAccount databaseAccount;

    public AssignmentDatabaseAccount(string nameSuffix, AssignmentResourceGroup rg, List<string> replicationLocations)
    {
        databaseAccount = new DatabaseAccountBuilder(nameSuffix)
            .InitializeArgs()
            .WithResourceGroup(rg.Name)
            .WithLocation(rg.Location)
            .WithDatabaseAccountOfferType(DatabaseAccountOfferType.Standard)
            .WithReplicationLocations(replicationLocations)

            .Finalize()
            .Build();
    }

    public Pulumi.Output<string> Id => databaseAccount.Id;
    public Pulumi.Output<string> Name => databaseAccount.Name;
}
