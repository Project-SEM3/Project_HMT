using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMT.Models
{
    public class Request
    {
        public int Id { get; set; }
        [Required]
        public string? CodeRequest { get; set; }

        public char Request_Status { get; set; } // 0 - chưa duyệt; 1 - đồng ý, 2 - không đồng ý, 3 - thành công
        public int? TotalQuantity { get; set; }
        public double? TotalPrice { get; set; }
        public DateTime Create_at { get; set; }
        public bool IsDeleted { get; set; } = false;
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }

        [Required]
        public string UserManagerId { get; set; }
        [ForeignKey("UserManagerId")]
        public User? UserManager { get; set; }

        public ICollection<RequestDetail>? RequestDetails { get; set; }

        public Request()
        {
            CodeRequest = GenerateRandomCode();
        }

        private static string GenerateRandomCode(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
