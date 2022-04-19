using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Security.Claims;
using Tickets.Domain.Entities;
using Tickets.Extensions;
using Tickets.Services.Interfaces;
using Tickets.ViewModel;

namespace Tickets.Controllers
{
    public class TicketController : Controller
    {
        
        private Iservices<Wedstrijd> _wedstrijdService;
        private Iservices<Club> _clubService;
        private Iservices<Vak> _vakService;
        private Iservices<VakStadion> _vakStadionService;
        private Iservices<Aankopen> _aankopenService;
        private Iservices<Ticket> _ticketService;
        private Iservices<Plaat> _plaatsService;
        private Iservices<Abonnement> _abonnementService;
        private readonly IMapper _mapper;

        public TicketController(IMapper mapper,
            Iservices<Wedstrijd> wedstrijdservice, Iservices<Vak> vakservice,
            Iservices<VakStadion> vakstadionservice, Iservices<Aankopen> aankopenservice,
            Iservices<Ticket> ticketservice, Iservices<Plaat> plaatservice, Iservices<Abonnement> abonnementservice,
            Iservices<Club> clubservice)
        {

            _mapper = mapper;
            _wedstrijdService = wedstrijdservice;
            _vakService = vakservice;
            _vakStadionService = vakstadionservice;
            _aankopenService = aankopenservice;
            _ticketService = ticketservice;
            _plaatsService = plaatservice;
            _abonnementService = abonnementservice;
            _clubService = clubservice;
        }

        private List<Ticket> GetPlaats(IEnumerable<Ticket> tickets, int? VakId)
        {
            
            List<Ticket> p = new List<Ticket>();

            foreach (var i in tickets)
            {
                if (i.Plaats.VakId == VakId)
                {
                    p.Add(i);
                }
            }

            return p;
        }

        private int getHighestPlace(IEnumerable<Ticket> tickets, int? vakId)
        {
            var seatnr = 0;
            var seatnrSelected = 0;
            foreach(var i in tickets)
            {
                if(i.Plaats.VakId == vakId)
                {
                    if (seatnr == 0)
                    {
                        seatnr = (int)i.Plaats.Plaatsnummer;
                    } else
                    {
                        seatnrSelected = (int)i.Plaats.Plaatsnummer;
                    }

                    if(seatnrSelected > seatnr)
                    {
                        seatnr = seatnrSelected;
                    }
                }
            }
            return seatnr;
        }

        [HttpGet]
        public async Task<IActionResult> OrderCheck()
        {
            ShoppingCartVM? cartlist = HttpContext.Session.GetObject<ShoppingCartVM>("OrderCheck");

            return View(cartlist);
        }

        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            ShoppingCartVM? cartList
              = HttpContext.Session
              .GetObject<ShoppingCartVM>("OrderCheck");

            CartVM? itemToRemove =
                cartList?.Cart?.FirstOrDefault(r => r.WedstrijdId == id);
            

            if (itemToRemove != null)
            {
                cartList?.Cart?.Remove(itemToRemove);
                HttpContext.Session.SetObject("OrderCheck", cartList);

            }

            return View("OrderCheck", cartList);

        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Validate(List<CartVM> cart)
        {
            string? userID = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            TicketOrderVM ticketorder = new TicketOrderVM();

            Aankopen aankoop;
            Ticket ticket;
            Abonnement abonnement;
            Plaat plaats;

            try
            {
                foreach (var item in cart)
                {
                    aankoop = new Aankopen();
                    aankoop.ClientId = userID;
                    aankoop.Aankoopdatum = item.Aankoopdatum;
                    await _aankopenService.Add(aankoop);


                    if (item.IsAbonnement == true)
                    {
                        Club club = await _clubService.FindById(item.ThuisploegId,0);
                        List<int> seatnumbers = new List<int>();
                        IEnumerable<Wedstrijd> wedstrijdenAbo = await _wedstrijdService.FindThuisWedstrijd((int)item.ThuisploegId);
                        foreach(var j in wedstrijdenAbo)
                        {
                            IEnumerable<Ticket> ticketAbo = await _ticketService.FindThuisWedstrijd(j.WedstrijdId);
                            int highestplaceAbo = getHighestPlace(ticketAbo, item.VakId);
                            seatnumbers.Add(highestplaceAbo);
                            seatnumbers.Sort();
                        }

                        int highest = seatnumbers.Last();
                        abonnement = new Abonnement();
                        plaats = new Plaat();

                        plaats.Plaatsnummer = ++highest;
                        plaats.StadionId = club.StadionId;
                        plaats.VakId = item.VakId;
                        await _plaatsService.Add(plaats);

                        abonnement.AankoopId = aankoop.AankoopId;
                        abonnement.Stamnummer = item.ThuisploegId;
                        abonnement.PlaatsId = plaats.PlaatsId;
                        await _abonnementService.Add(abonnement);
                    } else
                    {
                        var aantalTicket = item.AantalTickets;

                        VakStadion stadionvak = await _vakStadionService.FindById(item.VakId, item.StadionId);

                        var capaciteit = stadionvak.Capaciteit;


                        IEnumerable<Ticket> ticketsBezet = await _ticketService.FindThuisWedstrijd((int)item.WedstrijdId);

                        var aantalBezet = GetPlaats(ticketsBezet, item.VakId).Count();


                        for (int i = 0; i < aantalTicket && aantalBezet <= capaciteit; i++)
                        {


                            IEnumerable<Ticket> tickets = await _ticketService.FindThuisWedstrijd((int)item.WedstrijdId);

                            int highestplace = getHighestPlace(tickets, item.VakId);

                            plaats = new Plaat();
                            ticket = new Ticket();

                            plaats.Plaatsnummer = ++highestplace;
                            plaats.StadionId = item.StadionId;
                            plaats.VakId = item.VakId;
                            await _plaatsService.Add(plaats);

                            ticket.AankoopId = aankoop.AankoopId;
                            ticket.PlaatsId = plaats.PlaatsId;
                            ticket.WedstrijdId = item.WedstrijdId;
                            await _ticketService.Add(ticket);

                        }
                    }                    
                }

            } 
            catch (DataException ex)
            {
                throw new DataException("error");
            } 
            catch (Exception ex)
            {
                throw new Exception("error");
            }
            
            return RedirectToAction("Index","Home");
        }
        
    }

}
