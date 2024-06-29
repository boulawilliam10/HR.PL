using HR.BLL.Interface;
using HR.DAL.Context;
using HR.DAL.Models;
using MVCCompany.BLL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.BLL.Repository
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(CompanyDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}
