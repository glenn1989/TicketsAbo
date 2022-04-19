using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tickets.Domain.Entities;
using Tickets.Services.Interfaces;
using System.Linq;
using Tickets.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tickets.Controllers
{


    public class AbonnementController : Controller
    {
        private Iservices<Club> _clubService;
        private Iservices<Wedstrijd> _wedstrijdService;
        private Iservices<Vak> _vakService;
        private Iservices<VakStadion> _vakStadionService;
        private readonly IMapper _mapper;

        public AbonnementController(IMapper mapper, Iservices<Club> clubservice,
            Iservices<Wedstrijd> wedstrijdservice, Iservices<Vak> vakservice, Iservices<VakStadion> vakstadionservice)
        {
            _mapper = mapper;
            _clubService = clubservice;
            _wedstrijdService = wedstrijdservice;
            _vakService = vakservice;
            _vakStadionService = vakstadionservice;
        }
        public async Task<IActionResult> Index(int id)
        {

            var list = await _wedstrijdService.FindThuisWedstrijd(id);
            List<WedstrijdlistVM> wedstrijdlistVMs = _mapper.Map<List<WedstrijdlistVM>>(list);
            WedstrijdVM wedstrijdVM = new WedstrijdVM();
            wedstrijdVM.Club = new SelectList(await _vakService.GetAll(), "VakId", "VakNaam", wedstrijdVM.Club);
            wedstrijdVM.wedstrijdlistVMs = wedstrijdlistVMs;
            return View(wedstrijdVM);
        }

        [HttpPost]

        public async Task<IActionResult> Index(WedstrijdVM entity, int id)
        {



            var sel = Convert.ToInt16(entity.VakId);

            if (sel == 2 || sel == 6)
            {
                TempData["status"] = "Dit vak is voorbehouden aan uitsupporters.";

            }

            var list = await _wedstrijdService.FindThuisWedstrijd(id);
            List<WedstrijdlistVM> wedstrijdlistVMs = _mapper.Map<List<WedstrijdlistVM>>(list);
            WedstrijdVM wedstrijdVM = new WedstrijdVM();
            wedstrijdVM.Club = new SelectList(await _vakService.GetAll(), "VakId", "VakNaam", wedstrijdVM.Club);
            wedstrijdVM.wedstrijdlistVMs = wedstrijdlistVMs;
            wedstrijdVM.VakId = entity.VakId;
            wedstrijdVM.ThuisploegId = id;


            return View(wedstrijdVM);
        }
    }
}

