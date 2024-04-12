using Pulumi.AzureNative.KeyVault;
using Pulumi.AzureNative.KeyVault.Inputs;
using Pulumi.AzureNative.Resources;
using System;
using System.Collections.Generic;


return await Pulumi.Deployment.RunAsync(() =>
{
    var timestamp = DateTimeOffset.UtcNow.ToString("hhmmssff");
    var nameSuffix = $"pulumi-test-{timestamp}";
    var resourcesLocation = "polandcentral";
    // Create an Azure Resource Group
    var resourceGroup = new ResourceGroup("main-resource-group", new ResourceGroupArgs {
        ResourceGroupName = $"rg-{nameSuffix}",
        Location = resourcesLocation
    });

    var keyVault = new Vault("main-key-vault", new VaultArgs{
        VaultName = $"kv-{nameSuffix}",
        Location = resourcesLocation,
        ResourceGroupName = resourceGroup.Name,
        Properties = new VaultPropertiesArgs {
            EnablePurgeProtection = true,
            EnableSoftDelete = true,
            EnableRbacAuthorization = true,
            SoftDeleteRetentionInDays = 7,
            Sku = new SkuArgs {
                Family= SkuFamily.A,
                Name = SkuName.Standard
            },
            TenantId = "7b3de517-0e41-4b56-b895-9a8478923e20"
        }
    });

    // Create an Azure resource (Storage Account)
    // var storageAccount = new StorageAccount("sa", new StorageAccountArgs
    // {
    //     ResourceGroupName = resourceGroup.Name,
    //     Sku = new SkuArgs
    //     {
    //         Name = SkuName.Standard_LRS
    //     },
    //     Kind = Kind.StorageV2
    // });

    // var storageAccountKeys = ListStorageAccountKeys.Invoke(new ListStorageAccountKeysInvokeArgs
    // {
    //     ResourceGroupName = resourceGroup.Name,
    //     AccountName = storageAccount.Name
    // });

    // var primaryStorageKey = storageAccountKeys.Apply(accountKeys =>
    // {
    //     var firstKey = accountKeys.Keys[0].Value;
    //     return Output.CreateSecret(firstKey);
    // });

    // Export the primary key of the Storage Account
    return new Dictionary<string, object?>
    {
        ["KeyVaultId"] = keyVault.Id
    };
});