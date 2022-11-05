using System.ComponentModel.DataAnnotations;
namespace NguyenVietPhuongBTH2.Models
{
    public class Student
    {
        [Key]
        public string StudentID { get; set; }

        public string StudentName { get; set; }
    }
}
