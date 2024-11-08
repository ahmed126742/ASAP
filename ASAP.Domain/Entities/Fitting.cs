using ASAP.Domain.Entities.Common;

namespace ASAP.Domain.Entities
{
    public class Fitting : BaseEntity
    {
        public string? IstallerName { get; set; }

        public string? Comment { get; set; }

        public Guid? AttachementHeaderId { get; set; }

        public bool? AllFramesSquarLevelPlumb { get; set; }

        public bool? SaftyGlassInstallCorrectly { get; set; }

        public bool? InternalAndExternalMakingGoodComplete { get; set; }

        public bool? WindowDoorFramesAndGlassCleaned { get; set; }

        public bool? PropertyCleanedOfDebrisAndDust { get; set; }

        public bool? AllFixingsCorrectlyCarriedOut { get; set; }

        public bool? PhotosTaken { get; set; }

        public Guid? ContractItemId { get; set; }

        public ContractItem? ContractItem { get; set; }
    }
}
