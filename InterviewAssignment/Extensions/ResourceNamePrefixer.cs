namespace InterviewAssignmnet.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using Pulumi;
using Pulumi.AzureNative.Network;
using Pulumi.AzureNative.Resources;

public static class ResourceNamePrefixer
{
    private static string DEFAULT_RESOURCE_PREFIX = "";
    private static Dictionary<Type, string> _typeToPrefixTranslations = new()
    {
        { typeof(ResourceGroup), "rg-" },
        { typeof(VirtualNetwork), "vnet-" },
        { typeof(NetworkSecurityGroup), "nsg-" },
        { typeof(Subnet), "snet-" },
    };

    public static string AddPrefixIfRequired<T>(this string prefixable) where T: CustomResource {
        var prefix = _typeToPrefixTranslations.GetValueOrDefault(typeof(T), DEFAULT_RESOURCE_PREFIX);
        return prefixable.StartsWith(prefix) ? prefixable
            : string.Concat(prefix, prefixable);
    }
}