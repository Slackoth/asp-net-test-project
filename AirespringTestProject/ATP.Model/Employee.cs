using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATP.Model
{
    public class Employee
    {
        [Key, Column(name: "EmployeeID")]
        public long EmployeId { get; set; }
        [Required, Display(Name = "first name"), MaxLength(50, ErrorMessage = "Only 50 characters allowed"), RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Only letters allowed")]
        public string EmployeeFirstName { get; set; }
        [Required, Display(Name = "last name"), MaxLength(50, ErrorMessage = "Only 50 characters allowed"), RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Only letters allowed")]
        public string EmployeeLastName { get; set; }
        public string? EmployeePhone { get; set; }
        [Required, Display(Name = "zip code"), MinLength(9, ErrorMessage = "Must enter 9 digits"), MaxLength(9, ErrorMessage = "Only 9 digits allowed"), RegularExpression(@"^[0-9]+$", ErrorMessage = "Only numbers allowed")]
        public string EmployeeZip { get; set; }
        public DateTime HireDate { get; set; }

        public override string ToString() => "EmployeeID: " + EmployeId + ", Name: " + EmployeeFirstName + " " + EmployeeLastName + ", Phone: " + EmployeePhone + ", ZipCode: " + EmployeeZip + ", HireDate: " + HireDate;
    }
}
