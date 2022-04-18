using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tickets.Domain.Entities;
using Tickets.Services.Interfaces;
using Tickets.ViewModel;

namespace Tickets.Controllers
{
    public class HomeController : Controller
    {

        private Iservices<Club> _clubService;
        private Iservices<Wedstrijd> _wedstrijdService;

        private readonly IMapper _mapper;

        public HomeController(IMapper mapper, Iservices<Club> clubserivce, Iservices<Wedstrijd> wedstrijdservice)
        {
            _mapper = mapper;
            _clubService = clubserivce;
            _wedstrijdService = wedstrijdservice;

        }

        public async Task<IActionResult> Index()
        {
            var list = await _clubService.GetAll();
            List<ClubVM> listVM = _mapper.Map<List<ClubVM>>(list);
            return View(listVM);
        }
    }
}
