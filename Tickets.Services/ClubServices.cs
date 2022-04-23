using Tickets.Domain.Entities;
using Tickets.Repository.Interfaces;
using Tickets.Services.Interfaces;
using System;
using System.Linq;


namespace Tickets.Services
{
    public class ClubServices : Iservices<Club>
    {
        private IDAO<Club> _clubDAO;

        public ClubServices(IDAO<Club> clubDAO)
        {
            _clubDAO = clubDAO;
        }

        public async Task<IEnumerable<Club>> GetAll()
        {
            return await _clubDAO.GetAll();
        }

        public async Task<Club> FindById(int? id, int? id2=0)
        {
            return await _clubDAO.FindById(id,id2);
        }
        public async Task<IEnumerable<Club>> FindThuisWedstrijd(int id)
        {
            throw new NotImplementedException();
        }

        public Task Add(Club entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Club>> FindByOrder(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Club>> FindByUser(string id)
        {
            throw new NotImplementedException();
        }
    }
}