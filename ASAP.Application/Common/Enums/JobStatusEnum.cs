using System.ComponentModel;

namespace ASAP.Application.Common.Enums
{
    public enum JobStatusEnum
    {
        [Description("ToSurvey")]
        ToSurvey = 1,

        [Description("Surveyed")]
        Surveyed,

        [Description("ToProcess")]
        ToProcess,

        [Description("Processed")]
        Processed,

        [Description("PartiallyDelivered")]
        PartiallyDelivered,

        [Description("Delivered")]
        Delivered,

        [Description("ReadyToInstall")]
        ReadyToInstall,

        [Description("Booked")]
        Booked,

        [Description("Remarked")]
        Remarked,

        [Description("Complete")]
        Complete,

        [Description("OnHold")]
        OnHold
    }
}
