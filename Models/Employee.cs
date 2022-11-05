using System.ComponentModel.DataAnnotations;

namespace NguyenVietPhuongBTH2.Models
{
    public class Employee
    {
        [Key]
        public string EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        
    }
}
