using DSCC_CW1_Frontend_11733.Models;
using Microsoft.Extensions.Hosting;

namespace DSCC_CW1_Frontend_11733.Services
{
    public interface IApiService
    {
        // Topics API
        Task<IEnumerable<Topic>> GetAllTopics();
        Task<Topic> GetTopicById(int id);
        Task<IEnumerable<Article>> GetArticlesByTopicId(int id);
        Task DeleteTopicById(int id);
        Task CreateTopic (Topic topic);

        // Articles API
        Task<IEnumerable<Article>> GetAllArticles();
        Task<Article> GetArticleById(int id);
        Task CreateArticle(ArticleCreationDto articleCreationDto);
        Task UpdateTopic(Topic topic);
        Task UpdateArticle(int articleId, ArticleUpdateDto articleUpdateDto);
        Task DeleteArticleById(int id);
    }
}
