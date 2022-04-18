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
        private readonly IMapper _mapper;
        public ClubController(IMapper mapper, Iservices<Club> clubservice, Iservices<Wedstrijd> wedstrijdservice)
        {
            _mapper = mapper;
            _clubService = clubservice;
            _wedstrijdService = wedstrijdservice;
        }
        public async Task<IActionResult> Clubpage(int id,int id2)
        {

            if(id == null)
            {
                return NotFound();
            }

            Club club = await _clubService.FindById(id,id2);

            ClubVM clubvm = _mapper.Map<ClubVM>(club);

            return View(clubvm);
        }
    }
}
