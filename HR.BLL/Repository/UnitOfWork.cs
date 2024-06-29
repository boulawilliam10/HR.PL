using HR.BLL.Interface;
using HR.DAL.Context;
using MVCCompany.BLL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.BLL.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly CompanyDbContext _dbContext;

        public IEmployeeRepository? EmployeeRepository { get ; set ; }
        public IDepartmentRepository? DepartmentRepository { get ; set ; }
        public IProjectRepository? ProjectRepository { get ; set ; }
        public IWorks? Works { get ; set ; }

        public UnitOfWork(CompanyDbContext dbContext)
        {
            EmployeeRepository = new EmployeeRepository(dbContext);
            DepartmentRepository = new DepartmentRepository(dbContext);
            ProjectRepository = new ProjectRepository(dbContext);
            Works = new Works(dbContext);
            _dbContext = dbContext;
        }

        public async Task<int> Complete()
        => await _dbContext.SaveChangesAsync();

        public void Dispose()
        => _dbContext.Dispose();
        
    }
}
