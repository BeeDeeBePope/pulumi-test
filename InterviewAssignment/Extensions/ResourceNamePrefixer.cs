namespace InterviewAssignmnet.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pulumi;
using Pulumi.AzureNative.ManagedIdentity;
using Pulumi.AzureNative.Network;
using Pulumi.AzureNative.Resources;
using Pulumi.AzureNative.Web;

public static class ResourceNamePrefixer
{
    private static string DEFAULT_RESOURCE_PREFIX = "";
    private static Dictionary<Type, string> _typeToPrefixTranslations = new()
    {
        { typeof(ResourceGroup), "rg-" },
        { typeof(VirtualNetwork), "vnet-" },
        { typeof(NetworkSecurityGroup), "nsg-" },
        { typeof(Subnet), "snet-" },
        { typeof(AppServicePlan), "asp-" },
        { typeof(WebApp), "app-" },
        { typeof(UserAssignedIdentity), "id-" },
    };

    public static string AddPrefixIfRequired<T>(this string prefixable) where T: CustomResource {
        var prefix = _typeToPrefixTranslations.GetValueOrDefault(typeof(T), DEFAULT_RESOURCE_PREFIX);
        return prefixable.StartsWith(prefix) ? prefixable
            : string.Concat(prefix, prefixable);
    }
}

public static class StringExtensions {
    public static bool InNullOrEmpty(this string value){
        return string.IsNullOrEmpty(value);
    }
}