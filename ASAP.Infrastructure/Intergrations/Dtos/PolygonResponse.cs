namespace ASAP.Infrastructure.Intergrations.Dtos
{
    public class PolygonResponse
    {
        public List<Polygon> results { get; set; }
        public int ResultsCount { get; set; }
        public bool Adjusted { get; set; }
        public string Status { get; set; }
        public int? QueryCount { get; set; }
        public string request_id { get; set; }
    }

    public class Polygon
    {
        public string T { get; set; }
        public long t { get; set; }
        public decimal o { get; set; }
        public decimal h { get; set; }
        public decimal L { get; set; }
        public decimal c { get; set; }
        public decimal n { get; set; }
        public long v { get; set; }
        public decimal vw { get; set; }
    }
}
