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
    public class AbonnementServices : Iservices<Abonnement>
    {
        private IDAO<Abonnement> _abonnementDAO;

        public AbonnementServices(IDAO<Abonnement> abonnementDAO)
        {
            _abonnementDAO = abonnementDAO;
        }

        public async Task Add(Abonnement entity)
        {
            await _abonnementDAO.Add(entity);
        }

        public async Task<Abonnement> FindById(int? id, int? id2 = 0)
        {
            return await _abonnementDAO.FindById(id, id2);
        }

        public async Task<IEnumerable<Abonnement>> FindThuisWedstrijd(int id)
        {
           return await _abonnementDAO.FindThuisWedstrijd(id);
        }

        public Task<IEnumerable<Abonnement>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
