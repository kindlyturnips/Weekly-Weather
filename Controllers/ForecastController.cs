using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Weekly_Weather.Models;

namespace Weekly_Weather.Controllers
{
    [Authorize]
    [Route("/api/[controller]")]
    [ApiController]
    public class ForecastController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ForecastController> _logger;
        //private readonly Location location;


        public ForecastController(ApplicationDbContext context, UserManager<User> userManager, ILogger<ForecastController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;

        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            System.Diagnostics.Debug.AutoFlush = true;
            System.Diagnostics.Debug.WriteLine("Fucking Get");

            //var user = await _userManager.GetUserAsync(User);
            var userId = _userManager.GetUserId(User);
            var locations = await _context.Location
                                          .Where(l => l.UserId == userId)
                                          .ToListAsync();
            return Ok(locations);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Location location)
        {
            System.Diagnostics.Debug.AutoFlush = true;
            System.Diagnostics.Debug.WriteLine("Fucking Post");

            //Add UserID
            var userId = _userManager.GetUserId(User); // Get the current user's ID
            location.UserId = userId; // Set the UserModelId to associate the location with the current user

            _context.Location.Add(location);
            await _context.SaveChangesAsync();


            return CreatedAtAction("GetLocation", new { id = location.LocationId }, location);
        }

    }
}
