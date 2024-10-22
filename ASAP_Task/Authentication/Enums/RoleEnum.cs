using System.ComponentModel;

namespace ASAP_Task.WebAPI.Authentication.Enums
{
    public enum RoleEnum
    {
        [Description("System Admin")]
        SystemAdmin = 1,

        [Description("Fitter")]
        Fitter = 2,

        [Description("Surveyor")]
        Surveyor = 3
    }
}
