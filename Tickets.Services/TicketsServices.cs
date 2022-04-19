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
    public class TicketServices: Iservices<Ticket>
    {
        private IDAO<Ticket> _ticketDAO;

        public TicketServices(IDAO<Ticket> ticketDAO)
        {
            _ticketDAO = ticketDAO;
        }

        public async Task Add(Ticket entity)
        {
           await _ticketDAO.Add(entity);
        }

        public async Task<Ticket> FindById(int? id,int? id2=0)
        {
            return await _ticketDAO.FindById(id,id2);
        }

        public async Task<IEnumerable<Ticket>> FindThuisWedstrijd(int id)
        {
            return await _ticketDAO.FindThuisWedstrijd(id);
        }

        public Task<IEnumerable<Ticket>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
