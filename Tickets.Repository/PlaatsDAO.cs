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
    public class PlaatsDAO: IDAO<Plaat>
    {
        private readonly TicketsDbContext _ticketsDb;

        public PlaatsDAO()
        {
            _ticketsDb = new TicketsDbContext();
        }

        public async Task Add(Plaat entity)
        {
            _ticketsDb.Entry(entity).State = EntityState.Added;
            try
            {
                await _ticketsDb.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("error in DAO aankopen");
            }

        }

        public async Task<Plaat> FindById(int? id, int? id2)
        {
            try
            {
                return await _ticketsDb.Plaats.Where(a => a.VakId == id).Where(a => a.StadionId == id2).FirstOrDefaultAsync(); 
            }
            catch (Exception ex)
            {
                throw new Exception("fout in dao");
            }
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
            try
            {
                return await _ticketsDb.Plaats.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in PlaatsDAO");
            }
        }
    }
}
