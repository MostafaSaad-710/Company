using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.BLL.Interfaces;
using Company.DAL.Data.Contexts;
using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.BLL.Repositories
{
    public class EmployeeRepository :GenericRepository<Employee>, IEmployeeRepository
    {
        //public EmployeeRepository(CompanyDbContext context) : base(context)
        // {

        // }
        private readonly CompanyDbContext _context;

        public EmployeeRepository(CompanyDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Employee> GetByName(string name)
        {
            return _context.Employees.Include(E => E.Department).Where(E => E.Name.Contains(name.ToLower())).ToList();
        }
    }
}
