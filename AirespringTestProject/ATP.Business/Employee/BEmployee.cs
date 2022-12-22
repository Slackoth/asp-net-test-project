using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ATP.Business.Employee
{
    public interface BEmployee
    {
        int Add(Model.Employee Employee);
        int Edit(Model.Employee Employee);
        int Delete(long Id);
        IEnumerable<Model.Employee> FindAll();
        Model.Employee FindByEmployeeId(long Id);
        IEnumerable<Model.Employee> FindByEmployeeLastNameOrEmployeePhone(string Input);
    }
}
