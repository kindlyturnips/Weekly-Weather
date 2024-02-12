//////////////////////////////////////////////////////////////////////////////////////
//          STARTUP                                                                 //
//////////////////////////////////////////////////////////////////////////////////////

//Check & Refresh Forecasts
getLocation().then(locations => {
    var today = todayFormated();
    for (let i in locations) {
        getForecast(locations[i].locationId).then(forecast => {
            if (forecast[0].creation_date != today) {
                fetchWeatherForecast(locations[i].lat, locations[i].lon).then(new_forecast => {
                    putForecast(locations[i].locationId, new_forecast);
                });
            }
        });
    }
});

//////////////////////////////////////////////////////////////////////////////////////
//          SEARCH BAR                                                              //
//////////////////////////////////////////////////////////////////////////////////////


//Geocode Address
function geocodeAddress() {
    var address = document.getElementById('addressSearch').value;
    if (address !== '') {
        var geocodingUrl = 'https://nominatim.openstreetmap.org/search?format=json&q=' + encodeURIComponent(address);

        fetch(geocodingUrl)
            .then(response => response.json())
            .then(data => {
                if (data.length > 0) {
                    var result = data[0];

                    // Fetch location and then post it to the server
                    fetchLocation(result.lat, result.lon)
                        .then(location => {
                            return postLocation(location);
                        })
                        .catch(error => {
                            console.error('Error fetching location:', error);
                        });
                }
            })
            .catch(error => {
                console.error('Error during geocoding:', error);
                alert("An error occurred during geocoding.");
            });
    }
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
        daily: ['temperature_2m_max', 'temperature_2m_min', 'weathercode', 'sunrise', 'sunset', 'precipitation_sum','precipitation_probability_max'],
        timezone: 'auto'
    });

    try {
        const response = await fetch(`${endpoint}?${parameters}`);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        return await response.json();
    } catch (error) {
        console.error('Error fetching the weather data:', error);
    }
}



//////////////////////////////////////////////////////////////////////////////////////
//          CLIENT -> SERVER LOCATION FUNCTIONALITY                                 //
//////////////////////////////////////////////////////////////////////////////////////

// Function to get location data
async function getLocation() {
    try {
        const response = await fetch('/api/location/', { method: 'GET' });
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }

        const data = await response.json(); 
        return data; 
    } catch (error) {
        throw error; 
    }
}
// Function to post location data 
async function postLocation(location) {
    fetch('/api/location/', {
        method: "POST",
        headers: { 'Content-Type': "application/json" },
        body: JSON.stringify(createLocationData(location)),
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const locationUrl = response.headers.get('Location');
            return { response: response, locationUrl: locationUrl };
        })
        .then(({ response, locationUrl }) => {
            const locationId = locationUrl.split('/').pop();
            fetchWeatherForecast(location.lat, location.lon)
                .then(forecast => {
                    postForecast(locationId, forecast);
                })
                .then(data => {
                    window.location.reload(true);
                })
        })
        .catch(error => {
            console.log("Post Response:");
            console.log(JSON.stringify(createLocationData(location)));
            console.error('Error fetching data:', error);
        });
}

// Function to put location data 
async function putLocation(locationId, location) {
    fetch('/api/location/' + locationId, {
        method: 'PUT',
        headers: {'Content-Type': 'application/json',},
        body: JSON.stringify(createLocationData(location)),
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            console.log(response);
            return response;
        })
        .then(data => {
        })
        .catch(error => {
            console.error('Error fetching data:', error);
        });
}

// Function to delete location data
async function deleteLocation(locationId) {
    fetch('/api/location/' + locationId, {
        method: 'DELETE',
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
        }).then(data => {
            window.location.reload(true);
        });

}

//Function to format location data
function createLocationData(location) {
    var city = location.address.city || location.address.town || location.address.village || '';
    return {
        lat: location.lat,
        lon: location.lon,
        name: location.name,
        display_name: location.display_name,
        city: city,
        county: location.address.county,
        state: location.address.state,
        country: location.address.country,
        country_code: location.address.country_code
    };
}


//////////////////////////////////////////////////////////////////////////////////////
//           CLIENT -> SERVER FORECAST FUNCTIONALITY                                //
//////////////////////////////////////////////////////////////////////////////////////

// Function to get forecast data
async function getForecast(locationId) {
    try {
        const response = await fetch('/api/forecast/' + locationId, { method: 'GET' });

        if (!response.ok) {
            throw new Error('Network response was not ok');
        }

        const data = await response.json();
        console.log('Data received:', data);
        return data;
    } catch (error) {
        console.error('Error fetching data:', error);
        throw error;
    }
}

// Function to post forecast data
async function postForecast(locationId,forecast) {
    fetch('/api/forecast/' + locationId, {
        method: "POST",
        headers: { 'Content-Type': "application/json" },
        body: JSON.stringify(createForecastData(forecast)), 
    }).then(data => {
        window.location.reload(true);
    })
    return data

}

//Function to put forecast data
async function putForecast(locationId, forecast) {
    fetch('/api/forecast/' + locationId, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(createForecastData(forecast)),
    })
        .then(response => {
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
            console.error('Error fetching data:', error);
        });
}

// Function to delete forecast data
async function deleteForecast(locationId) {
    fetch('/api/forecast/' + locationId, {
        method: 'DELETE',
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
        });
}

//Function to format forecast data
function createForecastData(forecast) {

    const formattedDate = todayFormated();

    return {
        creation_date: formattedDate,
        date_array: forecast.daily.time,
        temperature_2m_max_array: forecast.daily.temperature_2m_max,
        temperature_2m_min_array: forecast.daily.temperature_2m_min,
        sunrise_array: forecast.daily.sunrise,
        sunset_array: forecast.daily.sunset,
        precipitation_sum_array: forecast.daily.precipitation_sum,
        precipitation_probability_max_array: forecast.daily.precipitation_probability_max,
        precipitation_sum_units: forecast.daily_units.precipitation_sum,
        temperature_2m_units: forecast.daily_units.temperature_2m_max,
        weather_code_array:forecast.daily.weathercode,
    };
}

//Function to format dates
function todayFormated() {
    const now = new Date(Date.now());

    // Extract year, month, and day
    const year = now.getFullYear();
    const month = (now.getMonth() + 1).toString().padStart(2, '0'); // Month is 0-indexed
    const day = now.getDate().toString().padStart(2, '0');

    // Format as 'YYYY-MM-DD'
    const formattedDate = `${year}-${month}-${day}`;
    return formattedDate;
}