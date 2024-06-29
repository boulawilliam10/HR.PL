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
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CompanyDbContext _dbcontext;
        public GenericRepository(CompanyDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task Add(T item)
        => await _dbcontext.Set<T>().AddAsync(item);


        public  void Delete(T item)
        =>  _dbcontext.Set<T>().Remove(item);


        public async Task<IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await _dbcontext.Employees.Include(E => E.Department).ToListAsync();
            }
            else if (typeof(T) == typeof(Project))
            {
                return (IEnumerable<T>) await _dbcontext.Projects.Include(P => P.Department).ToListAsync();
            }
            else if (typeof(T) == typeof(Work_For))
            {
                return (IEnumerable<T>) await _dbcontext.Works.Include(Y => Y.Employee).Include(X => X.Project).ToListAsync();
            }
            return await _dbcontext.Set<T>().ToListAsync();
        }


        public async Task<T> GetById(int id)
            => await _dbcontext.Set<T>().FindAsync(id);

        public void Update(T item)
        => _dbcontext.Update(item);

    }
}
