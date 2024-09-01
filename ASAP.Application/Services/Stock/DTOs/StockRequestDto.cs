namespace ASAP.Application.Services.Stock.DTOs
{
    public class StockRequestDto
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
