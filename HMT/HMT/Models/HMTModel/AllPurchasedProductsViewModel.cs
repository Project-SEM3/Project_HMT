namespace HMT.Models.HMTModel
{
    public class AllPurchasedProductsViewModel
    {
        public Category? Category { get; set; }
        public IEnumerable<Product>? Products { get; set; }
        public IEnumerable<TotalProduct>? TotalProducts { get; set; }
    }
}
