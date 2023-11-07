using DSCC_CW1_Frontend_11733.Models;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Text;

namespace DSCC_CW1_Frontend_11733.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public ApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiBaseUrl"];
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        // Authors
        public async Task<IEnumerable<Topic>> GetAllTopics()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Topic>>(_apiBaseUrl + "Topic");
        }

        public async Task<Topic> GetTopicById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Topic>(_apiBaseUrl + $"Topic/{id}");
        }

        public async Task<IEnumerable<Article>> GetArticlesByTopicId(int id)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Article>>(_apiBaseUrl + $"Topic/{id}/Articles");
        }

        // Posts
        public async Task<IEnumerable<Article>> GetAllArticles()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Article>>(_apiBaseUrl + $"Article");
        }

        public async Task<Article> GetArticleById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Article>(_apiBaseUrl + $"Article/{id}");
        }

        public async Task CreateTopic(Topic topic)
        {
            var json = JsonSerializer.Serialize(topic);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_apiBaseUrl + "Topic", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task CreateArticle(ArticleCreationDto article)
        {
            var json = JsonSerializer.Serialize(article);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_apiBaseUrl + "Article", content);
            response.EnsureSuccessStatusCode();
        }

        // Method to update a topic
        public async Task UpdateTopic(Topic topic)
        {
            var json = JsonSerializer.Serialize(topic);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(_apiBaseUrl + $"Topic/{topic.Id}", content);
            response.EnsureSuccessStatusCode();
        }


        // Method to update a article
        public async Task UpdateArticle(int articleId, ArticleUpdateDto articleUpdateDto)
        {
            var json = JsonSerializer.Serialize(articleUpdateDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(_apiBaseUrl + "Article/" + articleId, content);
            response.EnsureSuccessStatusCode();
        }

        // Method to delete a article by ID
        public async Task DeleteArticleById(int id)
        {
            var response = await _httpClient.DeleteAsync(_apiBaseUrl + $"Article/{id}");
            response.EnsureSuccessStatusCode();
        }

        // Method to delete an topic by Id
        public async Task DeleteTopicById(int id)
        {
            var response = await _httpClient.DeleteAsync(_apiBaseUrl + $"Topic/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
