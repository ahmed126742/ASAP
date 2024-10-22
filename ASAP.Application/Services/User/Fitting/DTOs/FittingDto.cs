namespace ASAP.Application.Services.User.Fitting.DTOs
{
    public class FittingDto
    {
        public string? IstallerName { get; set; }

        public string? Comment { get; set; }

        public Guid AttachementHeaderId { get; set; }

        public bool AllFramesSquarLevelPlumb { get; set; }

        public bool SaftyGlassInstallCorrectly { get; set; }

        public bool InternalAndExternalMakingGoodComplete { get; set; }

        public bool WindowDoorFramesAndGlassCleaned { get; set; }

        public bool PropertyCleanedOfDebrisAndDust { get; set; }

        public bool AllFixingsCorrectlyCarriedOut { get; set; }

        public bool PhotosTaken { get; set; }

    }
}
