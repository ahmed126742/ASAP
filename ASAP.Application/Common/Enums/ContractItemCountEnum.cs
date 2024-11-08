using System.ComponentModel;

namespace ASAP.Application.Common.Enums
{
    public enum ContractItemCountEnum
    {
        [Description("View All")]
        ViewAll = 1,

        [Description("View Completed")]
        ViewCompleted = 2,

        [Description("View Remake")]
        ViewRemake = 3,

        [Description("View Incomplete")]
        ViewIncomplete = 4,

        [Description("View OnHold")]
        viewOnHold = 6
    }
}
