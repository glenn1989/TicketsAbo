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
    public class WedstrijdDAO : IDAO<Wedstrijd>
    {
        private readonly TicketsDbContext _ticketsDb;

        public WedstrijdDAO()
        {
            _ticketsDb = new TicketsDbContext();
        }

        public async Task<IEnumerable<Wedstrijd>> GetAll()
        {
            try
            {
                return await _ticketsDb.Wedstrijds
                    .Include(a => a.Thuisploeg)
                    .Include(a => a.Uitploeg)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error in dao");
                throw new Exception("error in dao");
            }
        }

        public async Task<IEnumerable<Wedstrijd>> FindThuisWedstrijd(int id)
        {
            try
            {
                return await _ticketsDb.Wedstrijds
                    .Include(a => a.Thuisploeg)
                    .Include(a => a.Uitploeg)
                    .Where(b => b.ThuisploegId == id)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("error DAO");
            }
        }

        public async Task<Wedstrijd> FindById(int? id, int? id2 = 0)
        {
            try
            {
                return await _ticketsDb.Wedstrijds
                    .Include(a => a.Thuisploeg)
                    .Include(a => a.Uitploeg)
                    .Where(b => b.WedstrijdId == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("error DAO");
            }
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

        public Task Delete(Wedstrijd entity)
        {
            throw new NotImplementedException();
        }
    }
}
