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
    public class VakDAO : IDAO<Vak>
    {

        private readonly TicketsDbContext _ticketsDb;

        public VakDAO()
        {
            _ticketsDb = new TicketsDbContext();
        }

        public Task Add(Vak entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Vak entity)
        {
            throw new NotImplementedException();
        }

        public Task<Vak> FindById(int? id, int? id2 = 0)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Vak>> FindByOrder(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Vak>> FindByUser(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Vak>> FindThuisWedstrijd(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Vak>> GetAll()
        {
            try
            {
                return await _ticketsDb.Vaks.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("error DAO");
            }
        }
    }
}
