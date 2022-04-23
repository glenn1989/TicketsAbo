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
    public class WedstrijdServices : Iservices<Wedstrijd>
    {
        private IDAO<Wedstrijd> _wedstrijdDAO;

        public WedstrijdServices(IDAO<Wedstrijd> wedstrijdDAO)
        {
            _wedstrijdDAO = wedstrijdDAO;
        }

        public async Task<IEnumerable<Wedstrijd>> GetAll()
        {
            return await _wedstrijdDAO.GetAll();
        }

        public async Task<IEnumerable<Wedstrijd>> FindThuisWedstrijd(int id)
        {
            return await _wedstrijdDAO.FindThuisWedstrijd(id);
        }

        public async Task<Wedstrijd> FindById(int? id, int? id2=0)
        {
            return await _wedstrijdDAO.FindById(id,id2);
        }

        public Task Add(Wedstrijd entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Wedstrijd>> FindByOrder(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Wedstrijd>> FindByUser(string id)
        {
            throw new NotImplementedException();
        }
    }
}
