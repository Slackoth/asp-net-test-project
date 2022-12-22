using ATP.Business.Employee;
using ATP.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AirespringTestProject.Pages.Employee
{
    public class EditModel : PageModel
    {
        private readonly BEmployee Business;
        [BindProperty]
        public ATP.Model.Employee Employee { get; set; }
        [BindProperty, Required, Display(Name = "phone"), MinLength(8, ErrorMessage = "Must enter 11 (dialing code and number) digits"), MaxLength(8, ErrorMessage = "Only 11 (dialing code and number) digits allowed"), RegularExpression(@"^[0-9]+$", ErrorMessage = "Only numbers allowed")]
        public string Phone { get; set; }
        [BindProperty, Required, Display(Name = "dialing code"), MinLength(3, ErrorMessage = "Must enter 3 digits"), MaxLength(3, ErrorMessage = "Only 3 digits allowed"), RegularExpression(@"^[0-9]+$", ErrorMessage = "Only numbers allowed")]
        public string DialingCode { get; set; }

        public EditModel(BEmployee Business)
        {
            this.Business = Business;
        }

        public void OnGet(long Id)
        {
            Employee = Business.FindByEmployeeId(Id);
            
            if(Employee.EmployeePhone != null)
            {
                DialingCode = Employee.EmployeePhone.Substring(0, 3);
                Phone = Employee.EmployeePhone.Substring(3, 8);
            }
        }

        public IActionResult OnPost()
        {
            Employee.EmployeePhone = string.Concat(DialingCode, Phone);

            if (ModelState.IsValid)
            {
                Business.Edit(Employee);
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
