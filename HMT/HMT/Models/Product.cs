using System.ComponentModel.DataAnnotations;

namespace HMT.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string NameProduct { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public ICollection<RequestDetail>? RequestDetails { get; set; }
    }
}
