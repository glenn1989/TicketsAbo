using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets.Services.Interfaces
{
    public interface Iservices<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<IEnumerable<T>> FindThuisWedstrijd(int id);

        Task<T> FindById(int? id, int? id2);
        Task Add(T entity);

        Task<IEnumerable<T>> FindByOrder(int id);

        Task<IEnumerable<T>> FindByUser(string id);
    }
}
