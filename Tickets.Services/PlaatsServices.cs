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
    public class PlaatsServices : Iservices<Plaat>
    {
        private IDAO<Plaat> _plaatsDAO;

        public PlaatsServices(IDAO<Plaat> plaatsdao)
        {
            _plaatsDAO = plaatsdao;
        }

        public async Task Add(Plaat entity)
        {
             await _plaatsDAO.Add(entity);
        }

        public async Task Delete(Plaat entity)
        {
            await _plaatsDAO.Delete(entity);
        }

        public async Task<Plaat> FindById(int? id, int? id2)
        {
            return await _plaatsDAO.FindById(id, id2);
        }

        public Task<IEnumerable<Plaat>> FindByOrder(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Plaat>> FindByUser(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Plaat>> FindThuisWedstrijd(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Plaat>> GetAll()
        {
            return await _plaatsDAO.GetAll();
        }
    }



}
