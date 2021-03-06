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

        private int getHighestAboPlace(IEnumerable<Abonnement> aboBezet, int? vakId)
        {
            int highestAbo = 0;
            List<int> listAbo = new List<int>();

            foreach(var i in aboBezet)
            {
                if(i.Plaats.VakId == vakId)
                {
                    listAbo.Add((int)i.Plaats.Plaatsnummer);
                    listAbo.Sort();
                    highestAbo = listAbo.LastOrDefault();
                }
                
            }
            return highestAbo;
        }

        

        [HttpGet]
        [Authorize]
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

            CartVM? itemToRemove = cartList?.Cart?.FirstOrDefault();

            if (itemToRemove.IsAbonnement == true)
            {
               itemToRemove = cartList?.Cart?.FirstOrDefault(a => a.ThuisploegId == id);
            } else
            {
                itemToRemove =
               cartList?.Cart?.FirstOrDefault(r => r.WedstrijdId == id);
            }
           
            

            if (itemToRemove != null)
            {
                
                cartList?.Cart?.Remove(itemToRemove);
                HttpContext.Session.SetObject("OrderCheck", cartList);

            }
            

            return View("OrderCheck", cartList);

        }

        
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
            int aankoopID = 0;


            aankoop = new Aankopen();
            aankoop.ClientId = userID;
            aankoop.Aankoopdatum = DateTime.Now;
            await _aankopenService.Add(aankoop);
            aankoopID = aankoop.AankoopId;

            try
            {
                foreach (var item in cart)
                {
                    


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


                        IEnumerable<Abonnement> aboBezet = await _abonnementService.FindThuisWedstrijd((int)item.ThuisploegId);

                        int highestAboPlace = getHighestAboPlace(aboBezet, item.VakId);


                        if(highest+1 == highestAboPlace)
                        {
                            plaats.Plaatsnummer = ++highestAboPlace;
                        } else if(highestAboPlace > highest)
                        {
                            plaats.Plaatsnummer = ++highestAboPlace;
                        } else
                        {
                            plaats.Plaatsnummer = ++highest;
                        }


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

                        Wedstrijd wedstrijd = await _wedstrijdService.FindById((int)item.WedstrijdId,0);

                       

                        var aantalBezet = GetPlaats(ticketsBezet, item.VakId).Count();


                        for (int i = 0; i < aantalTicket && aantalBezet <= capaciteit; i++)
                        {


                            IEnumerable<Ticket> tickets = await _ticketService.FindThuisWedstrijd((int)item.WedstrijdId);

                            IEnumerable<Abonnement> aboBezet = await _abonnementService.FindThuisWedstrijd((int)wedstrijd.ThuisploegId);



                            List<int> aboZitjes = new List<int>();

                            foreach (var az in aboBezet.Where(a => a.Plaats.VakId == item.VakId))
                            {
                                aboZitjes.Add((int)az.Plaats.Plaatsnummer);
                            }

                            List<int> TicketZitjes = new List<int>();

                            foreach(var tz in tickets.Where(a => a.Plaats.VakId == item.VakId))
                            {
                                TicketZitjes.Add((int)tz.Plaats.Plaatsnummer);
                            }

                            int highestAboPlace = getHighestAboPlace(aboBezet, item.VakId);

                            int highestplace = getHighestPlace(tickets, item.VakId);



                            plaats = new Plaat();
                            ticket = new Ticket();

                            int zitplaats = 0;


                            while (aboZitjes.Contains(zitplaats+1) || TicketZitjes.Contains(zitplaats+1))
                            {
                                ++zitplaats;
                            }

                            plaats.Plaatsnummer = ++zitplaats;
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

            HttpContext.Session.Clear();
            return RedirectToAction("Overview", new { id = aankoopID });
        }

        public async Task<IActionResult> Overview(int id)
        {
            OverviewVM overview;
            OverviewListVM listVM = new OverviewListVM();
            listVM.OverviewList = new List<OverviewVM>();
            IEnumerable<Ticket> ticketOrder = await _ticketService.FindByOrder(id);
            IEnumerable<Abonnement> aboOrder = await _abonnementService.FindByOrder(id);

            

            foreach (var i in ticketOrder)
            {

                overview = new OverviewVM();
                Club club = await _clubService.FindById(i.Wedstrijd.ThuisploegId, 0);
                Club club2 = await _clubService.FindById(i.Wedstrijd.UitploegId, 0);
                overview.Thuisploeg = club.Clubnaam;
                overview.Uitploeg = club2.Clubnaam;
                overview.Vaknummer = (int)i.Plaats.VakId;
                overview.Pleknummer = (int)i.Plaats.Plaatsnummer;
                overview.TicketID = i.TicketId;
                
                listVM.OverviewList.Add(overview);
                
            }


            foreach (var i in aboOrder)
            {
                overview = new OverviewVM();
                overview.Thuisploeg = i.StamnummerNavigation.Clubnaam;
                overview.isAbo = true;
                overview.Vaknummer = (int)i.Plaats.VakId;
                overview.Pleknummer = (int)i.Plaats.Plaatsnummer;
                overview.TicketID = i.Abonnementsnummer;
                listVM.OverviewList.Add(overview);

            }


            return View(listVM);
        }


        public async Task<IActionResult> DeleteTicket(int id)
        {
            
            if(id == null)
            {
                return NotFound();
            }

            DeleteVM deleteVM = new DeleteVM();

            


            try
            {
                Ticket ticket = await _ticketService.FindById(id, 0);
                Plaat plaats = await _plaatsService.FindById(ticket.PlaatsId, 0);
                Aankopen aankoop = await _aankopenService.FindById(ticket.AankoopId, 0);


                deleteVM.TicketID = id;
                deleteVM.PlaatsID = (int)ticket.PlaatsId;
                deleteVM.OrderID = (int)ticket.AankoopId;

                var ticketlist = await _ticketService.FindByOrder(aankoop.AankoopId);
                var abolist = await _abonnementService.FindByOrder(aankoop.AankoopId);

                await _plaatsService.Delete(plaats);
                await _ticketService.Delete(ticket);

                if(ticketlist.Count() == 1 && abolist.Count() == 0)
                {
                    await _aankopenService.Delete(aankoop);
                }

            }
            catch(Exception ex)
            {
                throw new Exception("Kan entiteit niet verwijderen.");
            }
            

            return View(deleteVM);
        }
    }

}
