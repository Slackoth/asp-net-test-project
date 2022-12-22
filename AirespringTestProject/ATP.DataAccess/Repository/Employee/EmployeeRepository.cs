using System.Data;

namespace ATP.DataAccess.Repository.Employee
{
    public interface EmployeeRepository
    {
        int Add(string LastName, string FirstName, string Phone, string ZipCode);
        int Edit(string LastName, string FirstName, string Phone, string ZipCode, long Id);
        int Delete(long Id);
        DataTable FindAll();
        DataTable FindByEmployeeId(long Id);
        DataTable FindByEmployeeLastNameOrEmployeePhone(string Like);
    }
}
