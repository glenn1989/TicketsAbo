using AutoMapper;
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
        public async Task<IActionResult> Select(TicketVM entityVM, int id, int id2)
        {
            if (id == null)
            {
                return NotFound();
            }

            VakStadion vakStadion = new VakStadion(); 

            if(entityVM.wedstrijdID == null)
            {
                Club club = await _clubService.FindById(id2, 0);
                IEnumerable<Wedstrijd> wedstrijds = await _wedstrijdService.FindThuisWedstrijd(id2);
                vakStadion = await _vakStadionService.FindById(id, club.StadionId);
            }

            Wedstrijd wedstrijd = await _wedstrijdService.FindById(id, id2);
            vakStadion = await _vakStadionService.FindById(entityVM.VakId, id2);

            CartVM item = new CartVM
            {
                WedstrijdId = id,
                AantalTickets = entityVM.aantalTickets,
                Prijs = (float)vakStadion.Prijs,
                Aankoopdatum = DateTime.Now,
                Stadion = vakStadion.Stadion.StadionNaam,
                StadionId = vakStadion.StadionId,
                Thuisploeg = wedstrijd.Thuisploeg.Clubnaam,
                Uitploeg = wedstrijd.Uitploeg.Clubnaam,
                VakId = vakStadion.VakId

            };


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
            shopping.Cart.Add(item);

            HttpContext.Session.SetObject("OrderCheck", shopping);


            return RedirectToAction("OrderCheck", "Ticket");
        }
    }
}
