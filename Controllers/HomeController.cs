using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Diagnostics;
using Weekly_Weather.Models;

namespace Weekly_Weather.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, UserManager<User> userManager, ILogger<HomeController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public IActionResult Index()
        {

            var userId = _userManager.GetUserId(User);
            var locations =  GetLocations().Result;
            System.Diagnostics.Debug.AutoFlush = true;
            System.Diagnostics.Debug.WriteLine("Home Controller");

            foreach(var location in locations)
            {
                System.Diagnostics.Debug.WriteLine(location.city);
                System.Diagnostics.Debug.WriteLine(location);
                System.Diagnostics.Debug.WriteLine(location.forecast[0].sunrise_array);
            }


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
                                          .Where(l => l.LocationId == location.LocationId)
                                          .ToListAsync();
            }
                return locations;
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        

    }
}