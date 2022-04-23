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
    public class AankopenDAO: IDAO<Aankopen>
    {
        private readonly TicketsDbContext _ticketsDb;

        public AankopenDAO()
        {
            _ticketsDb = new TicketsDbContext();
        }

        public async Task<Aankopen> FindById(int? id, int? id2=0)
        {
            try
            {
                return await _ticketsDb.Aankopens.Where(a => a.AankoopId == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("error in dao");
            }
            
        }

        public Task<IEnumerable<Aankopen>> FindThuisWedstrijd(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Aankopen>> GetAll()
        {
            try
            {
                return await _ticketsDb.Aankopens.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("error in DAO aankpen");
            }
        }

        public async Task Add(Aankopen entity)
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

        public async Task<IEnumerable<Aankopen>> FindByOrder(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Aankopen>> FindByUser(string id)
        {
            try
            {
                return await _ticketsDb.Aankopens.Include(a => a.Client).Include(a => a.Abonnements).Include(a => a.Tickets).Where(a => a.ClientId.Equals(id)).ToListAsync();
            } catch (Exception ex)
            {
                throw new Exception("Error in AankopenDAO");
            }
        }

    }
}
