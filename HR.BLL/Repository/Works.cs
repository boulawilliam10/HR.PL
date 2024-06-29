using HR.BLL.Interface;
using HR.DAL.Context;
using HR.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.BLL.Repository
{
    public class Works : GenericRepository<Work_For>,IWorks
    {
        public Works(CompanyDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}
