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
    public class VakStadionServices : Iservices<VakStadion>
    {
        

        private IDAO<VakStadion> _VakStadionDAO;

        public VakStadionServices(IDAO<VakStadion> vakstadion)
        {
            _VakStadionDAO = vakstadion;
        }

        public Task Add(VakStadion entity)
        {
            throw new NotImplementedException();
        }

        public async Task<VakStadion> FindById(int? id,int? id2)
        {
            return await _VakStadionDAO.FindById(id,id2);
        }

        public Task<IEnumerable<VakStadion>> FindByOrder(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VakStadion>> FindByUser(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VakStadion>> FindThuisWedstrijd(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<VakStadion>> GetAll()
        {
            return await _VakStadionDAO.GetAll();
        }
    }
}
