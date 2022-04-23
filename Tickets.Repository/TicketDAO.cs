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
    public class TicketDAO : IDAO<Ticket>
    {

        private readonly TicketsDbContext _ticketsDb;

        public TicketDAO()
        {
            _ticketsDb = new TicketsDbContext();
        }
        public async Task Add(Ticket entity)
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

        public async Task<Ticket> FindById(int? id, int? id2 = 0)
        {
            try
            {
                return await _ticketsDb.Tickets.Where(a => a.TicketId == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("error in dao");
            }
        }

        public async Task<IEnumerable<Ticket>> FindThuisWedstrijd(int id)
        {
            try
            {
                return await _ticketsDb.Tickets.Include(a => a.Plaats).Include(a => a.Wedstrijd).Where(a => a.WedstrijdId == id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ticketDAO");
            }
        }

        public async Task<IEnumerable<Ticket>> FindByOrder(int id)
        {
            try
            {
                return await _ticketsDb.Tickets.Include(a => a.Plaats).Include(a => a.Wedstrijd).Where(a => a.AankoopId == id).ToListAsync();
            } 
            catch (Exception ex)
            {
                throw new Exception("Error in ticketDAO");
            }
        }

        public Task<IEnumerable<Ticket>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Ticket>> FindByUser(string id)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(Ticket entity)
        {
            _ticketsDb.Entry(entity).State = EntityState.Deleted;
            try
            {
                await _ticketsDb.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("error in PlaatsDAO");
            }
        }
    }
}
