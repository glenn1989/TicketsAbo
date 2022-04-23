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
    public class VakServices : Iservices<Vak>
    {
        private IDAO<Vak> _VakDAO;
        public VakServices(IDAO<Vak> vakDAO)
        {
            _VakDAO = vakDAO;
        }

        public Task Add(Vak entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Vak entity)
        {
            throw new NotImplementedException();
        }

        public Task<Vak> FindById(int? id, int? id2)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Vak>> FindByOrder(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Vak>> FindByUser(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Vak>> FindThuisWedstrijd(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Vak>> GetAll()
        {
            return await _VakDAO.GetAll();
        }
    }
}
