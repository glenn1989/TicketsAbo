using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Domain.Entities;
using Tickets.Repository.Interfaces;
using Tickets.Services.Interfaces;

namespace Tickets.Services
{
    public class AankopenServices: Iservices<Aankopen>
    {
        private IDAO<Aankopen> _aankopenDAO;
        public AankopenServices(IDAO<Aankopen> aankopenDAO)
        {
            _aankopenDAO = aankopenDAO;
        }

        public async Task Add(Aankopen entity)
        {
            await _aankopenDAO.Add(entity);
        }

        public async Task<Aankopen> FindById(int? id, int? id2 = 0)
        {
            return await _aankopenDAO.FindById(id,id2);
        }

        public Task<IEnumerable<Aankopen>> FindByOrder(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Aankopen>> FindThuisWedstrijd(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Aankopen>> GetAll()
        {
            return _aankopenDAO.GetAll();
        }
    }
}
