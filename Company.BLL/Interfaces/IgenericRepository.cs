using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.DAL.Models;

namespace Company.BLL.Interfaces
{
    public interface IgenericRepository<T> where T : BasicEntity
    {
        IEnumerable<T> GetAll();
        T? Get(int id);
        void Add(T model);
        void Update(T model);
        void Delete(T model);
    }
}
