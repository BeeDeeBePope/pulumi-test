namespace InterviewAssignmnet.Utility;

public static class Config
{
    public static class Network
    {
        private static Pulumi.Config cfg = new Pulumi.Config("network");

        public static string VnetAddressSpace => cfg.Require("vnetAddressSpace");
        public static string FuncAppSubnetAddressSpace => cfg.Require("funcAppSubnetAddressSpace");
        public static string AppServiceSubnetAddressSpace => cfg.Require("appServiceSubnetAddressSpace");

    }
}