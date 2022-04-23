using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Domain.Data;
using Tickets.Domain.Entities;
using Tickets.Repository.Interfaces;

namespace Tickets.Repository
{
    public class VakStadionDAO : IDAO<VakStadion>
    {
        private readonly TicketsDbContext _ticketDb;

        public VakStadionDAO()
        {
            _ticketDb = new TicketsDbContext();
        }

        public Task Add(VakStadion entity)
        {
            throw new NotImplementedException();
        }

        public async Task<VakStadion> FindById(int? id,int? id2)
        {
            try
            {
                return await _ticketDb.VakStadions.Include(a => a.Vak).Include(a => a.Stadion).Where(a => a.VakId == id).Where(a => a.StadionId == id2).FirstOrDefaultAsync();
            }
            catch(Exception ex)
            {
                throw new Exception("fout in dao");
            }
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
            try
            {
                return await _ticketDb.VakStadions.ToListAsync();
            }
            catch(Exception ex)
            {
                throw new Exception("error in dao");
            }
        }
    }
}
