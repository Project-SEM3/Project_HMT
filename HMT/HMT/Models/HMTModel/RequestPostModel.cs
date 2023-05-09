namespace HMT.Models.HMTModel
{
    public class RequestPostModel
    {
        public int? manager { get; set; }
        public int? requestId { get; set; }
        public List<int>? requestDetails { get; set; }
        public List<int>? category { get; set; }
        public List<int>? product { get; set; }
        public List<int>? quantity { get; set; }
        public List<string>? note { get; set; }

    }
}
