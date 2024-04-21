namespace InterviewAssignmnet.Extensions;

using System;
using System.Collections.Generic;
using Pulumi;
using Pulumi.AzureNative.DocumentDB;
using Pulumi.AzureNative.Insights;
using Pulumi.AzureNative.ManagedIdentity;
using Pulumi.AzureNative.Network;
using Pulumi.AzureNative.Resources;
using Pulumi.AzureNative.Storage;
using Pulumi.AzureNative.Web;

public static class ResourceNamePrefixer
{
    private static readonly string DEFAULT_RESOURCE_PREFIX = "";
    private static readonly Dictionary<Type, string> _typeToPrefixTranslations =
        new()
        {
            { typeof(DatabaseAccount), "cosmos-" },
            { typeof(Pulumi.AzureNative.DocumentDB.PrivateEndpointConnection), "pec-" },
            { typeof(DiagnosticSetting), "" },
            { typeof(UserAssignedIdentity), "id-" },
            { typeof(VirtualNetwork), "vnet-" },
            { typeof(NetworkSecurityGroup), "nsg-" },
            { typeof(Subnet), "snet-" },
            { typeof(PrivateEndpoint), "pe-" },
            { typeof(ResourceGroup), "rg-" },
            { typeof(BlobContainer), "" },
            { typeof(StorageAccount), "sa" },
            { typeof(AppServicePlan), "asp-" },
            { typeof(WebApp), "app-" },
            { typeof(WebAppPrivateEndpointConnection), "apppec-" },
        };

    public static string AddPrefixIfRequired<T>(this string prefixable)
        where T : CustomResource
    {
        var prefix = _typeToPrefixTranslations.GetValueOrDefault(
            typeof(T),
            DEFAULT_RESOURCE_PREFIX
        );
        return prefixable.StartsWith(prefix) ? prefixable : string.Concat(prefix, prefixable);
    }
}

public static class ResourceNameFormatter
{
    private static readonly Dictionary<Type, Func<string, string>> _typeToFormatTranslations =
        new() { { typeof(StorageAccount), (string formattable) => formattable.Replace("-", "") }, };

    private static Func<string, string> DEFAULT_RESOURCE_FORMATTER = (string s) => s;

    public static string ApplySpecialFormattingIfRequired<T>(this string formattable)
        where T : CustomResource
    {
        var formatter = _typeToFormatTranslations.GetValueOrDefault(
            typeof(T),
            DEFAULT_RESOURCE_FORMATTER
        );
        return formatter(formattable);
    }
}

public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string value)
    {
        return string.IsNullOrEmpty(value);
    }
}
