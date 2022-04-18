using Microsoft.EntityFrameworkCore;
using Tickets.Domain.Data;
using Tickets.Domain.Entities;
using System.Linq;
using Tickets.Repository.Interfaces;

namespace Tickets.Repository
{
    public class ClubsDAO : IDAO<Club>
    {
        private readonly TicketsDbContext _ticketsDb;

        public ClubsDAO()
        {
            _ticketsDb = new TicketsDbContext();
        }

        public async Task<IEnumerable<Club>> GetAll()
        {
            try
            {
                return await _ticketsDb.Clubs
                    .Include(a => a.Stadion).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error in dao");
                throw new Exception("error in dao");
            }
        }

        public async Task<Club> FindById(int? id, int? id2 = 0)
        {
            try
            {
                return await _ticketsDb.Clubs.Where(b => b.Stamnummer == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("error in DAO");
            }
        }

        public async Task<IEnumerable<Club>> FindThuisWedstrijd(int id)
        {
            throw new NotImplementedException();
        }

        public Task Add(Club entity)
        {
            throw new NotImplementedException();
        }
    }
}