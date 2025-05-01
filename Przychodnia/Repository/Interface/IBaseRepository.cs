using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Repository.Interface;

internal interface IBaseRepository<T> where T : class
{
    void Add(T entity);
    void Remove(T entity);
    IEnumerable<T> GetAll();
    T? GetById(int id);

    void SaveChanges();
}
