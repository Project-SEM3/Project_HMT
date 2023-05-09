using PagedList.Core;

namespace HMT.Models.HMTModel
{
    public class UserRequestModel
    {
        public int? requestId { get; set; }
        public IEnumerable<User>? Users { get; set; }
        public IEnumerable<Product>? Products { get; set; }
        public IEnumerable<Request>? Requests { get; set; }
        public IEnumerable<RequestDetail>? RequestDetails { get; set; }
        public IPagedList<Request>? PagedRequests { get; set; }
    }
}
