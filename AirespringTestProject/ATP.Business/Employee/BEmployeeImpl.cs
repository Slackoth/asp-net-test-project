using ATP.DataAccess.Repository.Employee;
using ATP.Util;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace ATP.Business.Employee
{
    public class BEmployeeImpl : BEmployee
    {
        private readonly EmployeeRepository Repository;

        public BEmployeeImpl(EmployeeRepository Repository)
        {
            this.Repository = Repository;
        }

        public int Add(Model.Employee Employee)
        {
            return Repository.Add(Employee.EmployeeLastName, Employee.EmployeeFirstName, Employee.EmployeePhone, Employee.EmployeeZip);
        }

        public int Delete(long Id)
        {
            return Repository.Delete(Id);
        }

        public int Edit(Model.Employee Employee)
        {
            return Repository.Edit(Employee.EmployeeLastName, Employee.EmployeeFirstName, Employee.EmployeePhone, Employee.EmployeeZip, Employee.EmployeId);
        }

        public IEnumerable<Model.Employee> FindAll()
        {
            return DataTableToEmployee(Repository.FindAll());
        }

        public Model.Employee FindByEmployeeId(long Id)
        {
            List<Model.Employee> Employees = DataTableToEmployee(Repository.FindByEmployeeId(Id));

            if (Employees.IsNullOrEmpty())
                return new();
            
            return DataTableToEmployee(Repository.FindByEmployeeId(Id))[0];
        }

        public IEnumerable<Model.Employee> FindByEmployeeLastNameOrEmployeePhone(string Input)
        {
            string Like = '%' + ATPUtil.RemoveDashAndParenthesis(Input) + '%';

            return DataTableToEmployee(Repository.FindByEmployeeLastNameOrEmployeePhone(Like));
        }

        private List<Model.Employee> DataTableToEmployee(DataTable DataTable)
        {
            int Rows = DataTable.Rows.Count;
            List<Model.Employee> Employees = new();

            for (int i = 0; i < Rows; i++)
            {
                Model.Employee Employee = new()
                {
                    EmployeId = Convert.ToInt64(DataTable.Rows[i]["EmployeeID"]),
                    EmployeeLastName = Convert.ToString(DataTable.Rows[i]["EmployeeLastName"]),
                    EmployeeFirstName = Convert.ToString(DataTable.Rows[i]["EmployeeFirstName"]),
                    EmployeePhone = Convert.ToString(DataTable.Rows[i]["EmployeePhone"]),
                    EmployeeZip = Convert.ToString(DataTable.Rows[i]["EmployeeZip"]),
                    HireDate = Convert.ToDateTime(DataTable.Rows[i]["HireDate"])
                };

                Employees.Add(Employee);
            }
            
            return Employees;
        }
    }
}
