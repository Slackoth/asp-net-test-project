using ATP.Business.Employee;
using ATP.DataAccess;
using ATP.DataAccess.Repository.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace AirespringTestProject.Pages.Employee
{
    public class IndexModel : PageModel
    {
        private readonly BEmployee Business;

        public IEnumerable<ATP.Model.Employee> Employees { get; set; }

        public IndexModel(BEmployee Business)
        {
            this.Business = Business;
        }

        public void OnGet()
        {
            Employees = Business.FindAll();
        }

        public void OnGetSearchEmployee(string SearchInput)
        {
            Employees = Business.FindByEmployeeLastNameOrEmployeePhone(SearchInput);
        }

        public IActionResult OnPostDeleteEmployee(long Id)
        {
            int Rows = Business.Delete(Id);
            return new JsonResult(Rows > 0 ? "EmployeeID deleted: " + Id : "Employee not deleted");
        }
    }
}
