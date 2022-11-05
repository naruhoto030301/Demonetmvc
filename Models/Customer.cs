using System.ComponentModel.DataAnnotations;
namespace NguyenVietPhuongBTH2.Models
{
    public class Customer
    {
        [Key]
        public string CustomerID { get; set; }

        public string CustomerName { get; set; }

        
    }
}
