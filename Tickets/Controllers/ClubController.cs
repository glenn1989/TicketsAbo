using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tickets.Domain.Entities;
using Tickets.Services.Interfaces;
using Tickets.ViewModel;

namespace Tickets.Controllers
{
    public class ClubController : Controller
    {
        private Iservices<Club> _clubService;
        private Iservices<Wedstrijd> _wedstrijdService;
        private Iservices<Abonnement> _abonnementService;
        private readonly IMapper _mapper;
        public ClubController(IMapper mapper, Iservices<Club> clubservice, Iservices<Wedstrijd> wedstrijdservice, Iservices<Abonnement> abonnementservice)
        {
            _mapper = mapper;
            _clubService = clubservice;
            _wedstrijdService = wedstrijdservice;
            _abonnementService = abonnementservice;
        }
        public async Task<IActionResult> Clubpage()
        {


            ClubVM clubvm;
            ClubListVM clublist = new ClubListVM();
            clublist.clublist = new List<ClubVM>();

            IEnumerable<Club> clubs = await _clubService.GetAll();

            foreach(var i in clubs)
            {
                clubvm = new ClubVM();
                var alleAbos = await _abonnementService.FindThuisWedstrijd(i.Stamnummer);

                int varAboploeg = alleAbos.Where(a => a.Stamnummer == i.Stamnummer).Count();
                clubvm.Clubnaam = i.Clubnaam;
                clubvm.Abonnees = varAboploeg;
                clubvm.Stadion = i.Stadion.StadionNaam;
                clubvm.Capaciteit = i.Stadion.StadionCapaciteit;
                clublist.clublist.Add(clubvm);
            }

            return View(clublist);
        }
    }
}
