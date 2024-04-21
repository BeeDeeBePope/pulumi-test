using System.Collections.Generic;
using Pulumi;
using Pulumi.AzureNative.Storage;

namespace InterviewAssignmnet.CustomResources.Builders.Storage;


public class StorageAccountBuilder : AzureResourceBuilder<StorageAccount, StorageAccountArgs>
{
    private readonly StorageAccountArgs args = new();

    public StorageAccountBuilder(string nameSuffix)
        : base(nameSuffix) { }

    public override StorageAccount Build() => new StorageAccount(Name, args);

    public override StorageAccountArgsBuilder InitializeArgs() =>
        new StorageAccountArgsBuilder(this, args);
}

public class StorageAccountArgsBuilder
    : AzureResourceArgsBuilder<StorageAccount, StorageAccountArgs>
{
    private readonly StorageAccountArgs args;


    public StorageAccountArgsBuilder(StorageAccountBuilder resourceBuilder, StorageAccountArgs args)
        : base(resourceBuilder)
    {
        this.args = args;
        this.args.AccountName = resourceBuilder.Name;
        this.args.Kind = Kind.StorageV2;
        this.args.Sku = new Pulumi.AzureNative.Storage.Inputs.SkuArgs(){
            Name = SkuName.Standard_LRS
        };
    }

    public StorageAccountArgsBuilder WithLocation(Pulumi.Input<string> azureLocation)
    {
        args.Location = azureLocation;
        return this;
    }

    public StorageAccountArgsBuilder WithResourceGroup(Pulumi.Input<string> rgName)
    {
        args.ResourceGroupName = rgName;
        return this;
    }

    // public StorageAccountArgsBuilder WithOsKind(Pulumi.Input<string> osKind)
    // {
    //     args.Kind = osKind;
    //     args.Reserved = osKind == (Input<string>)"Linux";
    //     return this;
    // }

    // public StorageAccountArgsBuilder WithSku(Input<AspSku> aspSku)
    // {
    //     args.Sku = SkuDescriptionArgs[aspSku];
    //     return this;
    // }
}
