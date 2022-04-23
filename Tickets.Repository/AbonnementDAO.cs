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
    public class AbonnementDAO : IDAO<Abonnement>
    {

        private readonly TicketsDbContext _ticketsDb;

        public AbonnementDAO()
        {
            _ticketsDb = new TicketsDbContext();
        }
        public async Task Add(Abonnement entity)
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

        public async Task<Abonnement> FindById(int? id, int? id2 = 0)
        {
            try
            {
                return await _ticketsDb.Abonnements.Where(a => a.Abonnementsnummer == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("error in dao");
            }
        }

        public async Task<IEnumerable<Abonnement>> FindByOrder(int id)
        {
            try
            {
                return await _ticketsDb.Abonnements.Include(a => a.Plaats).Include(a => a.StamnummerNavigation).Where(a => a.AankoopId == id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in AbonnementDAO");
            }
        }

        public Task<IEnumerable<Abonnement>> FindByUser(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Abonnement>> FindThuisWedstrijd(int id)
        {
            try
            {
                return await _ticketsDb.Abonnements.Include(a => a.Plaats).Where(a => a.Stamnummer == id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("error in dao");
            }
        }

        public Task<IEnumerable<Abonnement>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
