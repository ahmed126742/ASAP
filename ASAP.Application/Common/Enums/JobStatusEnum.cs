using System.ComponentModel;

namespace ASAP.Application.Common.Enums
{
    public enum JobStatusEnum
    {
        [Description("ToSurvey")]
        ToSurvey = 1,

        [Description("ToProcess")]
        ToProcess,

        [Description("Processed")]
        Processed,

        [Description("ReadyToInstall")]
        ReadyToInstall,

        [Description("Booked")]
        Booked,

        [Description("Remake")]
        Remake,

        [Description("Installed")]
        Installed,

        [Description("Complete")]
        Complete,

        [Description("OnHold")]
        OnHold
    }
}
