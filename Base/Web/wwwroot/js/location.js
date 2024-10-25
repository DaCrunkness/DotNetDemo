const locationButton = document.getElementById('getLocationBtn');
const coordinatesElement = document.getElementById('coordinates');
const addressElement = document.getElementById('address');

// Replace with your OpenCage API key
const openCageApiKey = '6b0552afb9db4c8998e8693bc8220b6f';

locationButton.addEventListener('click', () => {
    // Check if Geolocation is available
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(successCallback, errorCallback);
    } else {
        alert("Geolocation is not supported by this browser.");
    }
});

// Success callback for geolocation
function successCallback(position) {
    const latitude = position.coords.latitude;
    const longitude = position.coords.longitude;

    // Update the coordinates on the page
    coordinatesElement.textContent = `Coordinates: Latitude ${latitude}, Longitude ${longitude}`;

    // Reverse geocode to get human-readable address using OpenCage API
    const geocodeUrl = `https://api.opencagedata.com/geocode/v1/json?q=${latitude}+${longitude}&key=${openCageApiKey}`;

    fetch(geocodeUrl)
        .then(response => response.json())
        .then(data => {
            // Update address from reverse geocoding result
            const address = data.results[0].formatted;
            addressElement.textContent = `Address: ${address}`;
        })
        .catch(error => {
            console.error('Error fetching address:', error);
            addressElement.textContent = 'Address: Could not retrieve address';
        });
}

// Error callback for geolocation
function errorCallback(error) {
    console.error('Error getting location:', error);
    alert("Unable to retrieve your location.");
}