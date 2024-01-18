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
                    city: request.term 
                },
                success: function (data) {
                    var uniqueLabels = {};
                    var inputTerm = request.term.toLowerCase();

                    var results = $.map(data, function (item) {
                        // Check if the item is in the United States
                        if (item.address && item.address.country_code === 'us') {
                            // Focus on city, town, or village
                            var label = item.address.city || item.address.town || item.address.village || '';

                            // Check if the label starts with the input term
                            if (label.toLowerCase().startsWith(inputTerm) && !uniqueLabels[label]) {
                                uniqueLabels[label] = true;
                                return { label: label, value: label };
                            }
                        }
                    });

                    // Pass the filtered array of results to the response callback
                    response(results);
                }

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
