using HR.DAL.Models;

namespace HR.BLL.Interface
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        public IQueryable<Employee> GetEmpByName(string empName); 
    }
}
