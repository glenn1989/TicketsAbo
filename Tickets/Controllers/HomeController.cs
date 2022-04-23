using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using System.Threading.Tasks;
using Tickets.Extensions;
using Tickets.Domain.Entities;
using Tickets.Services.Interfaces;
using Tickets.ViewModel;

namespace Tickets.Controllers
{
    public class HomeController : Controller
    {

        private Iservices<Club> _clubService;
        private Iservices<Wedstrijd> _wedstrijdService;
        private Iservices<Aankopen> _aankoopService;
        private Iservices<Ticket> _ticketService;
        private Iservices<Abonnement> _abonnementService;

        private readonly IMapper _mapper;

        public HomeController(IMapper mapper, Iservices<Club> clubserivce, Iservices<Wedstrijd> wedstrijdservice, Iservices<Aankopen> aankoopservice, Iservices<Ticket> ticketservice, Iservices<Abonnement> abonnementservice)
        {
            _mapper = mapper;
            _clubService = clubserivce;
            _wedstrijdService = wedstrijdservice;
            _aankoopService = aankoopservice;
            _ticketService = ticketservice;
            _abonnementService = abonnementservice;
        }

        public async Task<IActionResult> Index()
        {

            
            var list = await _clubService.GetAll();
            List<ClubVM> listVM = _mapper.Map<List<ClubVM>>(list);
            return View(listVM);
        }

        [Authorize]
        public async Task<IActionResult> MyOrders()
        {
            string? userID = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            IEnumerable<Aankopen> aankopenUser = await _aankoopService.FindByUser(userID);
            OverViewHashMap hashMap = new OverViewHashMap();

            hashMap.OverviewMap = new Dictionary<int, OverviewListVM>();




            foreach (var i in aankopenUser.OrderBy(a => a.Aankoopdatum).Reverse())
            {

                OverviewVM overview;
                OverviewListVM listVM = new OverviewListVM();
                listVM.OverviewList = new List<OverviewVM>();
               

                IEnumerable<Abonnement> aboUser = await _abonnementService.FindByOrder(i.AankoopId);
                IEnumerable<Ticket> ticketUser = await _ticketService.FindByOrder(i.AankoopId);
                

                foreach (var j in ticketUser)
                {

                    overview = new OverviewVM();
                    Club club = await _clubService.FindById(j.Wedstrijd.ThuisploegId, 0);
                    Club club2 = await _clubService.FindById(j.Wedstrijd.UitploegId, 0);
                    overview.Thuisploeg = club.Clubnaam;
                    overview.Uitploeg = club2.Clubnaam;
                    overview.Vaknummer = (int)j.Plaats.VakId;
                    overview.Pleknummer = (int)j.Plaats.Plaatsnummer;
                    overview.TicketID = j.TicketId;
                    overview.Wedstrijddatum = (System.DateTime)j.Wedstrijd.Datum;
                    listVM.OverviewList.Add(overview);
                    

                }
                

                foreach (var k in aboUser)
                {
                    overview = new OverviewVM();
                    overview.Thuisploeg = k.StamnummerNavigation.Clubnaam;
                    overview.isAbo = true;
                    overview.Vaknummer = (int)k.Plaats.VakId;
                    overview.Pleknummer = (int)k.Plaats.Plaatsnummer;
                    overview.TicketID = k.Abonnementsnummer;
                    listVM.OverviewList.Add(overview);
                    

                }
                hashMap.OverviewMap.Add(i.AankoopId, listVM);
            }

            return View(hashMap);


        }

    }




}
