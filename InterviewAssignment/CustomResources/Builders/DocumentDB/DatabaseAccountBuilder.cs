using System.Collections.Generic;
using System.Linq;
using Pulumi.AzureNative.DocumentDB;
using Pulumi.AzureNative.DocumentDB.Inputs;

namespace InterviewAssignmnet.CustomResources.Builders.DocumentDB;

public class DatabaseAccountBuilder : AzureResourceBuilder<DatabaseAccount, DatabaseAccountArgs>
{
    private readonly DatabaseAccountArgs args = new();

    public DatabaseAccountBuilder(string nameSuffix)
        : base(nameSuffix) { }

    public override DatabaseAccount Build() => new DatabaseAccount(Name, args);

    public override DatabaseAccountArgsBuilder InitializeArgs() =>
        new DatabaseAccountArgsBuilder(this, args);
}

public class DatabaseAccountArgsBuilder
    : AzureResourceArgsBuilder<DatabaseAccount, DatabaseAccountArgs>
{
    private readonly DatabaseAccountArgs args;

    public DatabaseAccountArgsBuilder(
        DatabaseAccountBuilder resourceBuilder,
        DatabaseAccountArgs args
    )
        : base(resourceBuilder)
    {
        this.args = args;
        this.args.AccountName = resourceBuilder.Name;
        this.args.PublicNetworkAccess = PublicNetworkAccess.Disabled;
    }

    public DatabaseAccountArgsBuilder WithLocation(Pulumi.Input<string> azureLocation)
    {
        args.Location = azureLocation;
        return this;
    }

    public DatabaseAccountArgsBuilder WithResourceGroup(Pulumi.Input<string> rgName)
    {
        args.ResourceGroupName = rgName;
        return this;
    }

    public DatabaseAccountArgsBuilder WithDatabaseAccountOfferType(
        DatabaseAccountOfferType offerType
    )
    {
        args.DatabaseAccountOfferType = offerType;
        return this;
    }

    public DatabaseAccountArgsBuilder WithReplicationLocations(List<string> locations)
    {
        args.Locations = locations
            .Select(
                (str, i) =>
                    new LocationArgs
                    {
                        FailoverPriority = i,
                        IsZoneRedundant = false,
                        LocationName = str
                    }
            )
            .ToList();
        return this;
    }

    // public DatabaseAccountArgsBuilder WithReplicationLocations(List<string> locations)
    // {
    //     args.
    //     return this;
    // }
}
