using HR.BLL.Interface;
using HR.DAL.Context;
using HR.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.BLL.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly CompanyDbContext _dbcontext;
        public EmployeeRepository(CompanyDbContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IQueryable<Employee> GetEmpByName(string empName)
        {
           
           return _dbcontext.Employees.Include(E => E.Department).Where(E => E.Name.ToLower().Contains(empName.ToLower()));

        }
    }
}
