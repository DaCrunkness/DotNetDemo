const fetchNewsButton = document.getElementById('fetchNewsBtn');
const keywordInput = document.getElementById('keyword');
const newsArticlesElement = document.getElementById('news-articles');

// Replace with your NewsAPI key
const apiKey = '794bb4926f9e493badf29345a16bb603';

fetchNewsButton.addEventListener('click', () => {
    const keyword = keywordInput.value;
    if (!keyword) {
        alert('Please enter a topic.');
        return;
    }

    const apiUrl = `https://newsapi.org/v2/everything?q=${keyword}&apiKey=${apiKey}`;

    // Fetch news articles based on the entered keyword
    fetch(apiUrl)
        .then(response => response.json())
        .then(data => {
            // Clear any previous results
            console.log(data)
            newsArticlesElement.innerHTML = '';

            if (data.articles.length === 0) {
                newsArticlesElement.innerHTML = '<p>No news articles found.</p>';
            } else {
                // Display the news articles
                data.articles.forEach(article => {
                    const articleElement = document.createElement('div');
                    articleElement.classList.add('article');

                    articleElement.innerHTML = `
                                <img src="${article.urlToImage || 'https://via.placeholder.com/300'}" alt="${article.title}">
                                <h3><a class="text-primary" href="${article.url}" target="_blank">${article.title}</a></h3>
                                <p class="text-primary">${article.description || 'No description available.'}</p>
                                <p class="text-primary"><strong>Source:</strong> ${article.source.name}</p>
                            `;

                    newsArticlesElement.appendChild(articleElement);
                });
            }
        })
        .catch(error => {
            console.error('Error fetching news:', error);
            newsArticlesElement.innerHTML = '<p>Error fetching news articles.</p>';
        });
});