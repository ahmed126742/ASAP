using ASAP.Domain.Entities.Common;

namespace ASAP.Domain.Entities
{
    public class Stock : BaseEntity
    {
        public string SymbolExchange { get; set; }
        public long TimeStamp { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal TransactionNo { get; set; }
        public long Volum { get; set; }
        public decimal VolumWeight { get; set; }
    }
}
