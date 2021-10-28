using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcProduct.Models.Data
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(int id);
        int Add(T item);
        bool Update(T item);
        bool Delete(int id);
    }
}
