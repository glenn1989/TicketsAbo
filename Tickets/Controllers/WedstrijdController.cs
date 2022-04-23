using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tickets.Domain.Entities;
using Tickets.Extensions;
using Tickets.Services.Interfaces;
using Tickets.ViewModel;

namespace Tickets.Controllers
{
    public class WedstrijdController : Controller
    {

        private Iservices<Club> _clubService;
        private Iservices<Wedstrijd> _wedstrijdService;
        private Iservices<Vak> _vakService;
        private Iservices<VakStadion> _vakStadionService;
        private readonly IMapper _mapper;

        public WedstrijdController(IMapper mapper, Iservices<Club> clubservice,
            Iservices<Wedstrijd> wedstrijdservice, Iservices<Vak> vakservice, Iservices<VakStadion> vakstadionservice)
        {
            _mapper = mapper;
            _clubService = clubservice;
            _wedstrijdService = wedstrijdservice;
            _vakService = vakservice;
            _vakStadionService = vakstadionservice;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            
            var list = await _wedstrijdService.GetAll();
            List<WedstrijdlistVM> wedstrijdlistVMs = _mapper.Map<List<WedstrijdlistVM>>(list);
            WedstrijdVM wedstrijdvm = new WedstrijdVM();
            wedstrijdvm.Club = new SelectList(await _clubService.GetAll(), "Stamnummer", "Clubnaam", wedstrijdvm.Club);
            wedstrijdvm.wedstrijdlistVMs = wedstrijdlistVMs;
            return View(wedstrijdvm);
        }

        [HttpPost]

        public async Task<IActionResult> Index(WedstrijdVM wVM)
        {

            var sel = Convert.ToInt32(wVM.Thuisploeg);
            var list = await _wedstrijdService.FindThuisWedstrijd(sel);
            List<WedstrijdlistVM> wedstrijdlistVMs = _mapper.Map<List<WedstrijdlistVM>>(list);
            wVM.wedstrijdlistVMs = wedstrijdlistVMs;
            wVM.Club = new SelectList(await _clubService.GetAll(), "Stamnummer", "Clubnaam", wVM.Club);

            return View(wVM);
        }

        public async Task<IActionResult> Ticketselect(int id, int id2)
        {

            if (id == null)
            {
                return NotFound();
            }

            Wedstrijd wedstrijd = await _wedstrijdService.FindById(id, id2);

            var ticket = new TicketVM();

            ticket.wedstrijdID = wedstrijd.WedstrijdId;
            ticket.Thuisploeg = wedstrijd.Thuisploeg.Clubnaam;
            ticket.Uitploeg = wedstrijd.Uitploeg.Clubnaam;
            ticket.StadionId = wedstrijd.Thuisploeg.StadionId;

            ticket.Vak = new SelectList(await _vakService.GetAll(), "VakId", "VakNaam", ticket.Vak);

            return View(ticket);
        }

        //[HttpPost]
        
        public async Task<IActionResult> Select(TicketVM entityVM, int id, int id2, int id3)
        {
            if (id == null)
            {
                return NotFound();
            }

            VakStadion vakStadion = new VakStadion();
            CartVM item = new CartVM();

            if (id3==0 || id3==null)
            {

                Club club = await _clubService.FindById(id, 0);
                IEnumerable<Wedstrijd> wedstrijds = await _wedstrijdService.FindThuisWedstrijd(id);
                vakStadion = await _vakStadionService.FindById(id2, club.StadionId);
                IEnumerable<Wedstrijd> list = await _wedstrijdService.FindThuisWedstrijd(id);
                List<Wedstrijd> wedstrijden = new List<Wedstrijd>();

                foreach(var i in list)
                {
                    wedstrijden.Add(i);
                }

                item.IsAbonnement = true;
                item.ThuisploegId = id;
                item.Thuisploeg = club.Clubnaam;
                item.VakId = id2;
                item.AantalTickets = 1;
                item.Prijs = (float)(vakStadion.Prijs * wedstrijden.Count());


            } else
            {
                Wedstrijd wedstrijd = await _wedstrijdService.FindById(id, id2);
                vakStadion = await _vakStadionService.FindById(entityVM.VakId, id2);


                item.WedstrijdId = id;
                item.AantalTickets = entityVM.aantalTickets;
                item.Prijs = (float)vakStadion.Prijs;
                item.Aankoopdatum = DateTime.Now;
                item.Stadion = vakStadion.Stadion.StadionNaam;
                item.StadionId = vakStadion.StadionId;
                item.Thuisploeg = wedstrijd.Thuisploeg.Clubnaam;
                item.Uitploeg = wedstrijd.Uitploeg.Clubnaam;
                item.VakId = vakStadion.VakId;
                item.Datum = (System.DateTime)wedstrijd.Datum;
            }


            ShoppingCartVM? shopping;

            if (HttpContext.Session.GetObject<ShoppingCartVM>("OrderCheck") != null)
            {
                shopping = HttpContext.Session.GetObject<ShoppingCartVM>("OrderCheck");
            }
            else
            {
                shopping = new ShoppingCartVM();
             
                shopping.Cart = new List<CartVM>();
            }

            if(item.IsAbonnement == true || shopping.Cart.Count() == 0)
            {
                shopping.Cart.Add(item);
            } else
            {
                foreach (var i in shopping.Cart.Where(a => a.IsAbonnement == null))
                {
                    int datetimeresult = DateTime.Compare((System.DateTime)i.Datum, (System.DateTime)item.Datum);

                    if (datetimeresult == 0)
                    {
                        TempData["Status"] = "Men kan maar voor 1 wedstrijd per speeldag tickets aankopen.";
                        return RedirectToAction("OrderCheck", "Ticket");

                    }

                }
                shopping.Cart.Add(item);
            }

            HttpContext.Session.SetObject("OrderCheck", shopping);


            return RedirectToAction("OrderCheck", "Ticket");
        }
    }
}
