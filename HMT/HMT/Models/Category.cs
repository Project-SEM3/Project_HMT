using System.ComponentModel.DataAnnotations;

namespace HMT.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string NameCategory { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
