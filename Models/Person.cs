using System.ComponentModel.DataAnnotations;

namespace NguyenVietPhuongBTH2.Models
{
    public class Person
    {
        [Key]
        public string PersonID { get; set; }

        public string PersonName { get; set; }

        public string PersonAddress { get; set; }
    }

}