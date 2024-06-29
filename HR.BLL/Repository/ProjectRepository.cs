using HR.DAL.Context;
using HR.BLL.Interface;
using HR.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.BLL.Repository;

namespace MVCCompany.BLL.Repository
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(CompanyDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}
