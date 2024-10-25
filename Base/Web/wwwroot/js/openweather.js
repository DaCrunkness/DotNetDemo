const apiKey = 'ce48aea3cbd21c5ffdf6366f49c0de41';  // Replace with your OpenWeatherMap API key
const city = 'Baton Rouge';                         // City for which you want to fetch weather data
const apiUrl = `https://api.openweathermap.org/data/2.5/weather?q=${city}&appid=${apiKey}&units=imperial`;

// Function to fetch data and update DOM
function fetchWeather() {
    fetch(apiUrl)
        .then(response => response.json())   // Parse the JSON response
        .then(data => {
            // Display weather information on the page
            console.log(data)
            const weatherElement = document.getElementById('weather');
            weatherElement.innerHTML = `
                        <h2>Weather in ${data.name}</h2>
                        <p>Temperature: ${data.main.temp} °F</p>
                        <p>Weather: ${data.weather[0].description}</p>
                    `;
        })
        .catch(error => console.error('Error fetching weather data:', error));
}

// Call the function to fetch weather data when the page loads
fetchWeather();