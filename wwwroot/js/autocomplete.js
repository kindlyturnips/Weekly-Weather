// This script sets up an autocomplete functionality for an address search input field
// using the Nominatim geocoding service.

// Ensure the DOM is fully loaded before executing the script
$(document).ready(function () {
    // Initialize the autocomplete feature on the input field with id 'addressSearch'
    $("#addressSearch").autocomplete({
        // The source option defines where to fetch the autocomplete suggestions from
        source: function (request, response) {
            // Perform an AJAX request to the Nominatim geocoding service
            $.ajax({
                url: "https://nominatim.openstreetmap.org/search?format=json",
                dataType: "json", // Expecting JSON data in response
                data: {
                    q: request.term,           // Search term entered by the user
                    countrycodes: 'us',       // Limit search to the United States
                    addressdetails: 1         // Request detailed address information
                },
                success: function (data) {
                    // Create an object to track unique labels (to avoid duplicates)
                    var uniqueLabels = {};

                    // Map the response data to a format suitable for autocomplete suggestions
                    var results = $.map(data, function (item) {
                        // Ensure the address is in the United States and contains necessary details
                        if (item.address && item.address.country_code === 'us') {

                            // Construct a label with the city/town name and state
                            var label = item.address.city || item.address.town || item.address.village || '';
                            if (item.address.state) {
                                label += (label ? ', ' : '') + item.address.state;
                            }

                            // Exclude entries with empty or duplicate labels
                            if (label && label !== item.address.state && !uniqueLabels[label]) {
                                uniqueLabels[label] = true; // Mark this label as processed
                                return { label: label, value: label };
                            }
                        }
                    });

                    // Pass the array of results to the response callback
                    response(results);
                }
            });
        },
        minLength: 3, // Minimum number of characters to trigger the autocomplete

        // Event handler for when a suggestion is selected
        select: function (event, ui) {
            console.log("Selected: " + ui.item.value);
        }
    });
});
