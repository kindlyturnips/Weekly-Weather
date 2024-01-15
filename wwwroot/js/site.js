// This script provides functionalities for geocoding addresses, retrieving weather forecasts, 
// and handling location data in a web application.

// Global array to store multiple locations
var locations = [];

//Update Location
document.addEventListener("DOMContentLoaded", function () {
    var forecastElements = document.querySelectorAll('.forecast-display');

    forecastElements.forEach(element => {
        var city = element.getAttribute('data-city');
        var latitude = element.getAttribute('data-lat');
        var longitude = element.getAttribute('data-lon');

        fetchWeatherForecast(latitude, longitude)
            .then(data => {
                // Handle the fetched data for each location
                displayForecast(data, element.querySelector('.forecast-data'));
            })
            .catch(error => {
                // Handle any errors
                console.error(error);
            });
    });
});


//////////////////////////////////////////////////////////////////////////////////////
//          SEARCH BAR AND API TESTING                                              //
//////////////////////////////////////////////////////////////////////////////////////

// Function to geocode an address entered by the user.
function geocodeAddress() {
    var address = document.getElementById('addressSearch').value;

    // Only proceed if the address field is not empty
    if (address !== '') {
        // Construct the URL for the geocoding request
        var geocodingUrl = 'https://nominatim.openstreetmap.org/search?format=json&q=' + encodeURIComponent(address);

        // Perform an AJAX request to the geocoding service
        fetch(geocodingUrl)
            .then(response => response.json())
            .then(data => {
                // Proceed only if there are results
                if (data.length > 0) {
                    var result = data[0]; // First result from the geocoding response

                    // Retrieve and process the location details
                    var location = fetchLocation(result.lat, result.lon).then(location => {
                        //Server API Requests
                        postLocation(location); // Send location details to the server
                        getLocation();
                    })
                    .catch(error => {
                        console.error('Error fetching location:', error);
                    });



                    // Convert the coordinates for map projection
                    var coord = ol.proj.fromLonLat([parseFloat(result.lon), parseFloat(result.lat)]);

                    // Fetch and process the weather forecast for the geocoded location
                    fetchWeatherForecast(result.lat, result.lon)
                        .then(weatherData => {
                            // Handle the weather data here
                            console.log(weatherData);
                        });
                } else {
                    // Alert if no results were found for the address
                    alert("Address not found!");
                }
            })
            .catch(error => {
                // Handle errors during geocoding
                console.error('Error during geocoding', error);
                alert("An error occurred during geocoding.");
            });
    }
}

 
async function Test() {
    console.log("TEST");
    const get_locations = await getLocation();
    console.log("Test Data 1:", get_locations);
    deleteLocation(get_locations[0].locationId)
    

}
//////////////////////////////////////////////////////////////////////////////////////
//          CLIENT -> EXTERNAL API FUNCTIONALITY                                    //
//////////////////////////////////////////////////////////////////////////////////////


// Function to retrieve detailed location information using reverse geocoding
async function fetchLocation(lat, lon) {
    var reverseGeocodingUrl = `https://nominatim.openstreetmap.org/reverse?format=json&lat=${lat}&lon=${lon}`;

    try {
        let response = await fetch(reverseGeocodingUrl);
        let data = await response.json();
        console.log("Location")
        console.log(data)
        return data;
    } catch (error) {
        console.error('Error during reverse geocoding:', error);
    }
}

// Function to fetch weather forecast data
async function fetchWeatherForecast(latitude, longitude) {
    const endpoint = 'https://api.open-meteo.com/v1/forecast';
    const parameters = new URLSearchParams({
        latitude: latitude,
        longitude: longitude,
        daily: ['temperature_2m_max', 'temperature_2m_min', 'weathercode'],
        timezone: 'auto'
    });

    try {
        const response = await fetch(`${endpoint}?${parameters}`);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        //console.log(response.json());
        return await response.json(); // Returns the weather forecast data
    } catch (error) {
        console.error('Error fetching the weather data:', error);
    }
}

function displayForecast(forecastData, element) {
    if (!forecastData || !element) return;

    // Clear existing content
    element.innerHTML = '';

    console.log(forecastData.daily);
    // Assuming the forecast data has a 'daily' property
    let dailyForecast = forecastData.daily;
    let time = dailyForecast.time;
    let maxTemps = dailyForecast.temperature_2m_max;
    let minTemps = dailyForecast.temperature_2m_min;
    let weatherCodes = dailyForecast.weathercode;

    for (let i = 0; i < maxTemps.length; i++) {
        let forecastElement = document.createElement('div');
        forecastElement.className = 'daily-forecast';
        forecastElement.dataset.weatherCode = weatherCodes[i];

        // Format the display of each day's forecast
        forecastElement.innerHTML = `
            <div class="forecast-day">
            <p>${time[i].slice(5)}:</p>
            <p>Max Temp: ${maxTemps[i]}°C</p>
            <p>Min Temp: ${minTemps[i]}°C</p>
            <p>Weather: ${weatherCodes[i]}</p>
            </div>
            
        `;
        //<p>Weather: ${getWeatherDescription(weatherCodes[i])}</p>

        element.appendChild(forecastElement);
    }
}

    // Populate the forecast data into the element
    // Example: element.innerHTML = 'Day 1: ' + forecastData.day1;



//////////////////////////////////////////////////////////////////////////////////////
//          CLIENT -> SERVER LOCATION FUNCTIONALITY                                 //
//////////////////////////////////////////////////////////////////////////////////////

// Function to send location data to a server
async function getLocation() {
    try {
        const response = await fetch('/api/location/', { method: 'GET' });
        console.log("GET REQUEST");

        if (!response.ok) {
            throw new Error('Network response was not ok');
        }

        const data = await response.json(); // Wait for the JSON parsing to complete
        console.log('Data received:', data);
        return data; // Return the parsed data
    } catch (error) {
        console.error('Error fetching data:', error);
        throw error; // Re-throw the error to be caught by the caller
    }
}
// Function to send location data to a server
async function postLocation(location) {
    fetch('/api/location/', {
        method: "POST",
        headers: { 'Content-Type': "application/json" },
        body: JSON.stringify(createLocationData(location)), // Send the location object directly
    })
        .then(response => {
            console.log("POST REQUEST");
            console.log(response.json());
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            console.log(response);
            return response;
        })
        .then(data => {
            console.log('Data received:', data);
        })
        .catch(error => {
            console.log("Post Response:");
            console.log(JSON.stringify(createLocationData(location)));
            console.error('Error fetching data:', error);
        });
}

// Function to send location data to a server
async function putLocation(locationId, location) {
    fetch('/api/location/' + locationId, {
        method: 'PUT',
        headers: {'Content-Type': 'application/json',},
        body: JSON.stringify(createLocationData(location)),
    })
        .then(response => {
            console.log("PUT REQUEST");
            console.log(response.json());
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            console.log(response);
            return response;
        })
        .then(data => {
            console.log('Data received:', data);
        })
        .catch(error => {
            console.log("Post Response:");
            console.log(JSON.stringify(createLocationData(location)));
            console.error('Error fetching data:', error);
        });
}

// Function to send location data to a server
async function deleteLocation(locationId) {
    fetch('/api/location/' + locationId, {
        method: 'DELETE',
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
        });

}

//Format  Location data
function createLocationData(location) {
    return {
        lat: location.lat,
        lon: location.lon,
        name: location.name,
        display_name: location.display_name,
        city: location.address.city,
        county: location.address.county,
        state: location.address.state,
        country: location.address.country,
        country_code: location.address.country_code
    };
}

//////////////////////////////////////////////////////////////////////////////////////
//           CLIENT -> SERVER FORECAST FUNCTIONALITY                                //
//////////////////////////////////////////////////////////////////////////////////////

async function postForecast(forecast) {
    fetch('/api/forecast/', {
        method: "POST",
        headers: { 'Content-Type': "application/json" },
        fetchWeatherForecast,
        body: JSON.stringify(forecast), // Send the location object directly

    })
        .then(response => {
            console.log("Post Request");
            console.log(response.json());
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            console.log(response);
            return response;
        })
        .then(data => {
            console.log('Data received:', data);
        })
        .catch(error => {
            console.log("Post Response:");
            console.log(JSON.stringify(forecast));
            console.error('Error fetching data:', error);
        });
}