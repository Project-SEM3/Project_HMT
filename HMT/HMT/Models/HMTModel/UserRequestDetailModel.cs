namespace HMT.Models.HMTModel
{
    public class UserRequestDetailModel
    {
        public User? UserFrom { get; set; }
        public User? UserTo { get; set; }
        public Request? Request { get; set; }
        public IEnumerable<Product>? Products { get; set; }
        public IEnumerable<RequestDetail>? RequestDetails { get; set; }
        public IEnumerable<Category>? Categories { get; set; }
    }
}
