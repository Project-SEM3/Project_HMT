namespace HMT.Models
{
    public class RequestDetail
    {
        public int Id { get; set; }

        public char Status { get; set; } // 0- chưa đưa, 1 - đã đưa
        public int Quantity { get; set; }
        public double? Price { get; set; }
        public string? Note { get; set; }
        public DateTime Create_at { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int RequestId { get; set; }
        public Request? Request { get; set; }

    }
}
