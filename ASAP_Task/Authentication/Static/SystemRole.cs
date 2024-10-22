namespace ASAP_Task.Authentication.Static 
{ 
    public static class SystemRole
    {
        public const string SystemAdmin = "System Admin";

        public const string Fitter = "Fitter";

        public const string Surveyor = "Surveyor";

        public static List<string> GetSystemRole()
        {
            return new List<string> { SystemAdmin, Fitter, Surveyor };
        }
    }
}
