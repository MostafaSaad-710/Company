using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.BLL.Interfaces;
using Company.DAL.Data.Contexts;
using Company.DAL.Models;

namespace Company.BLL.Repositories
{
    public class DepartmentRepsitory :GenericRepository<Department>, IDepartmentRepository
    {
       public DepartmentRepsitory(CompanyDbContext context) : base(context)
        {
        }
    }
}
