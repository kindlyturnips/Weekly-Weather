using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Weekly_Weather.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Linq;
using Microsoft.Build.Evaluation;
//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis;


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
            //Check
            System.Diagnostics.Debug.AutoFlush = true;
            System.Diagnostics.Debug.WriteLine("Get Location");

            //Add UserID
            var userId = _userManager.GetUserId(User);
            var locations = await _context.Location
                                          .Where(l => l.UserId == userId)
                                          .ToListAsync();
            return Ok(locations);
        }


        [HttpPost]
        public async Task<IActionResult> Post( Location location)
        {
            //Check
            System.Diagnostics.Debug.AutoFlush = true;
            System.Diagnostics.Debug.WriteLine("Post Location");

            //Add UserID
            var userId = _userManager.GetUserId(User); 
            location.UserId = userId;

            //Add Location
            _context.Location.Add(location);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Post Location", new { id = location.LocationId }, location);
        }


        // Update an existing location
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Location location)
        {
            //Check
            System.Diagnostics.Debug.AutoFlush = true;
            System.Diagnostics.Debug.WriteLine("Put Location");


            //Add UserID
            var userId = _userManager.GetUserId(User);
            //Find Location
            var existing_location = await _context.Location.FindAsync(id);
            if (existing_location == null)
            {
                return NotFound();
            }
            //Update
            existing_location.lat = location.lat;
            existing_location.lon = location.lon;
            existing_location.name = location.name;
            existing_location.display_name = location.display_name;
            existing_location.city = location.city;
            existing_location.county = location.county;
            existing_location.state = location.state;
            existing_location.country = location.country;
            existing_location.country_code = location.country_code;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Delete an existing location
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //Check
            System.Diagnostics.Debug.AutoFlush = true;
            System.Diagnostics.Debug.WriteLine("Delete Location");


            //Add UserID
            var userId = _userManager.GetUserId(User);
            //Find Location
            var existing_location = await _context.Location
                                          .Where(l => l.UserId == userId && l.LocationId == id)
                                          .FirstOrDefaultAsync();

            //Remove
            _context.Location.Remove(existing_location); // Remove the location from the context
            await _context.SaveChangesAsync(); // Save the changes to the database

            return NoContent();
        }
    }
}
