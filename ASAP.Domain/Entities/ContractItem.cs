using ASAP.Domain.Entities.Common;

namespace ASAP.Domain.Entities
{
    public class ContractItem : BaseEntity
    {
        public string? Address { get; set; }

        public int? Requirement { get; set; }

        public string? PostalCode { get; set; }

        public int? ProductionWeek { get; set; }

        public DateTime? InstallationDateFrom { get; set; }

        public DateTime? InstallationDateTo { get; set; }

        public int? Status { get; set; }

        public Guid? FitterId { get; set; }
        public Guid? SurveyorId { get; set; }

        public int? CertesNo { get; set; }
        public int? InvoiceNo { get; set; }

        public string? Notes { get; set; }

        public string? Frame { get; set; }
        public DateTime? RequestDate { get; set; }

        public int? W { get; set; }
        public int? RD { get; set; }
        public int? FD { get; set; }
        public Guid?  W_RD_FD_SupplierId { get; set; }
        public int? W_RD_FD_Status { get; set; }

        public int? PD { get; set; }
        public Guid? PD_SupplierId { get; set; }
        public int? PD_Status { get; set; }

        public int? VS { get; set; }
        public Guid? VS_SupplierId { get; set; }
        public int? VS_Status { get; set; }

        public int? FED { get; set; }
        public Guid? FED_SupplierId { get; set; }
        public int? FED_Status { get; set; }

        public int? Bifolds { get; set; }
        public Guid? Bifolds_SupplierId { get; set; }
        public int? Bifolds_Status { get; set; }

        public int? Roofs { get; set; }
        public Guid? Roofs_SupplierId { get; set; }
        public int? Roofs_Status { get; set; }
       
        public int? Ancils { get; set; }
        public Guid? Ancils_SupplierId { get; set; }
        public int? Ancils_Status { get; set; }

        public int? GlassStatus { get; set; }
        public DateTime? GlassDeliveryDate { get; set; }

        public int? PanelStatus { get; set; }
        public DateTime? PanelDeliveryDate { get; set; }

        public Guid ContractId { get; set; }
        public Contract? Contract { get; set; }

    }
}
