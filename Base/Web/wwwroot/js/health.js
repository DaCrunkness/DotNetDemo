const getNutritionButton = document.getElementById('getNutritionBtn');
const foodInput = document.getElementById('foodInput');
const foodInfoElement = document.getElementById('food-info');
const foodNameElement = document.getElementById('food-name');
const caloriesElement = document.getElementById('calories');
const fatElement = document.getElementById('fat');
const carbsElement = document.getElementById('carbs');
const proteinElement = document.getElementById('protein');

// Replace with your Nutritionix API credentials
const appId = 'fb064388';
const appKey = '27254d6190ccda6f5251bf050511ff06';

getNutritionButton.addEventListener('click', () => {
    const foodQuery = foodInput.value;
    if (!foodQuery) {
        alert('Please enter a food name.');
        return;
    }

    const apiUrl = `https://trackapi.nutritionix.com/v2/natural/nutrients`;

    // Fetch nutrition data from Nutritionix API
    fetch(apiUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'x-app-id': appId,
            'x-app-key': appKey,
        },
        body: JSON.stringify({
            query: foodQuery
        })
    })
        .then(response => response.json())
        .then(data => {
            const food = data.foods[0]; // We take the first result if multiple items are returned

            // Display the nutritional facts
            foodNameElement.textContent = food.food_name;
            caloriesElement.textContent = `${food.nf_calories} kcal`;
            fatElement.textContent = `${food.nf_total_fat} g`;
            carbsElement.textContent = `${food.nf_total_carbohydrate} g`;
            proteinElement.textContent = `${food.nf_protein} g`;

            // Make the table visible
            foodInfoElement.style.display = 'block';
        })
        .catch(error => {
            console.error('Error fetching nutrition data:', error);
            alert('Failed to fetch nutrition information.');
        });
});