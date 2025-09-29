using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Company.DAL.Models;

namespace Company.BLL.Interfaces
{
    public  interface IEmployeeRepository:IgenericRepository<Employee>
    {
        List<Employee> GetByName(string name);
        //IEnumerable<Employee> GetAll();
        //Employee? Get(int id);
        //int Add(Employee model);
        //int Update(Employee model);
        //int Delete(Employee model);
    }
}
