namespace HMT.Models
{
    public class TotalProduct
    {
        public int Id { get; set; }
        public int TotalQuantity { get; set; }
        public double TotalPrice { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
