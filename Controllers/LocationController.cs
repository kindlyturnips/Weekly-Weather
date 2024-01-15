using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Weekly_Weather.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Linq;
using Microsoft.Build.Evaluation;


namespace Weekly_Weather.Controllers
{
    [Authorize]
    [Route("/api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<LocationController> _logger;
        //private readonly Location location;


        public LocationController(ApplicationDbContext context, UserManager<User> userManager, ILogger<LocationController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;

        }


        // Get all locations for the current user
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


        // Update an existing location
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Location item)
        {
            var userId = _userManager.GetUserId(User); // Get the current user's ID
            var location = await _context.Location.FirstOrDefaultAsync(l => l.LocationId == id && l.UserId == userId);

            if (location == null)
            {
                return NotFound(); // Location not found or does not belong to user
            }

            // Update location properties here
            // location.Property = item.Property;
            // ...

            await _context.SaveChangesAsync();
            return Ok(location);
        }

        // Delete an existing location
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = _userManager.GetUserId(User); // Get the current user's ID
            var location = await _context.Location.FirstOrDefaultAsync(l => l.LocationId == id && l.UserId == userId);

            if (location == null)
            {
                return NotFound(); // Location not found or does not belong to user
            }

            _context.Location.Remove(location);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
