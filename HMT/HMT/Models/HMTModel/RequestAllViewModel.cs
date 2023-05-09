namespace HMT.Models.HMTModel
{
    public class RequestAllViewModel
    {
        public IEnumerable<Category>? Categories { get; set; }
        public IEnumerable<Product>? Products { get; set; }
        public IEnumerable<TotalProduct>? TotalProducts { get; set; }
        public IEnumerable<Request>? Requests { get; set; }
        public IEnumerable<RequestDetail>? RequestDetails { get; set; }
    }
}
