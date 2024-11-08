﻿using ASAP.Domain.Entities.Common;

namespace ASAP.Domain.Entities
{
    public class Survey : BaseEntity
    {
        public string? Location { get; set; }

        public int? Width { get; set; }

        public int? Height { get; set; }

        public int? IntlWidth { get; set; }

        public int? IntlHeight { get; set; }

        public int? Cill { get; set; }

        public int? Horns { get; set; }

        public string? Glass { get; set; }

        public string? Extras { get; set; }

        public Guid? AttachmentHeaderId { get; set; }

        public Guid? ContractItemId { get; set; }

        public ContractItem? ContractItem { get; set; }
    }
}
