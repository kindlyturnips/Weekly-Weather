using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Weekly_Weather.Models;

namespace Weekly_Weather.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //Home Page
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var locations = GetLocations().Result;
            locations.Reverse();
            return View(locations);
        }

        // Get all locations for the current user
        public async Task<List<Weekly_Weather.Models.Location>> GetLocations()
        {
            var userId = _userManager.GetUserId(User);
            var locations = await _context.Location
                                          .Where(l => l.UserId == userId)
                                          .ToListAsync();
            foreach (var location in locations)
            {
                location.forecast = await _context.Forecast
                                          .Where(l => l.ForecastId == location.LocationId)
                                          .ToListAsync();
            }
            return locations;
        }

        //Handle Errors
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }



}

 