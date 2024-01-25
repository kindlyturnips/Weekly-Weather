
// Autocomplete suggestions for Nominatim geocoding service.
$(document).ready(function () {
    $("#addressSearch").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "https://nominatim.openstreetmap.org/search?format=json",
                dataType: "json", 
                data: {
                    addressdetails:1,
                    city: request.term,  
                    countrycodes: 'us',     
                },
                success: function (data) {
                    var uniqueLabels = {};

                    // Map response data for autocomplete suggestions
                    var results = $.map(data, function (item) {
                        if (item.address && item.address.country_code === 'us') {
                            var label = item.address.city || item.address.town || item.address.village || '';
                            if (item.address.state) {
                                label += (label ? ', ' : '') + item.address.state;
                            }

                            if (label && label !== item.address.state && !uniqueLabels[label]) {
                                uniqueLabels[label] = true;
                                return { label: label, value: label };
                            }
                        }
                    });
                    response(results);
                }
            });
        },
        minLength: 3,
        select: function (event, ui) {
            console.log("Selected: " + ui.item.value);
        }
    });
});