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
        // Get all Forecasts for the Location
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            //Check
            System.Diagnostics.Debug.AutoFlush = true;
            System.Diagnostics.Debug.WriteLine("Get Forecast");

            //Add UserID
            var userId = _userManager.GetUserId(User);
            var forecasts = await _context.Forecast
                                          .Where(f => f.ForecastId == id)
                                          .ToListAsync();
            return Ok(forecasts);
        }


        [HttpPost("{id}")]
        public async Task<IActionResult> Post(int id, Forecast forecast)
        {
            //Check
            System.Diagnostics.Debug.AutoFlush = true;
            System.Diagnostics.Debug.WriteLine("Post Forecast");

            //Add UserID
            forecast.ForecastId = id;
            //forecast.ForecastId = id;

            //Add Forecast

            _context.Forecast.Add(forecast);
            await _context.SaveChangesAsync();
            //return CreatedAtAction("Post Forecast", forecast);
            return RedirectToAction("Index");
        }
        // Update an existing location
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Forecast forecast)
        {
            //Check
            System.Diagnostics.Debug.AutoFlush = true;
            System.Diagnostics.Debug.WriteLine("Put Forecast");

            
            //Add UserID
            forecast.ForecastId = id;
            //Find Location
            var existing_forecast = await _context.Forecast
                                           .FirstOrDefaultAsync(f => f.ForecastId == id);

            if (existing_forecast == null)
            {
                return NotFound();
            }
            //Update
            existing_forecast.date_array = forecast.date_array;
            existing_forecast.temperature_2m_max_array = forecast.temperature_2m_max_array;
            existing_forecast.temperature_2m_min_array = forecast.temperature_2m_min_array;
            existing_forecast.sunrise_array = forecast.sunrise_array;
            existing_forecast.sunset_array = forecast.sunset_array;
            existing_forecast.precipitation_sum_array = forecast.precipitation_sum_array;
            existing_forecast.precipitation_probability_max_array = forecast.precipitation_probability_max_array;
            existing_forecast.precipitation_sum_units = forecast.precipitation_sum_units;
            existing_forecast.temperature_2m_units = forecast.temperature_2m_units;
            await _context.SaveChangesAsync();
            
            return NoContent();
        }

        // Delete an existing location
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //Check
            System.Diagnostics.Debug.AutoFlush = true;
            System.Diagnostics.Debug.WriteLine("Delete Forecast");


            //Find Location
            var existing_forecast = await _context.Forecast
                                          .Where(l => l.ForecastId == id)
                                          .FirstOrDefaultAsync();

            //Remove
            _context.Forecast.Remove(existing_forecast); // Remove the location from the context
            await _context.SaveChangesAsync(); // Save the changes to the database

            return NoContent();
        }


    }
}
