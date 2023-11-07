using DSCC_CW1_Frontend_11733.Models;
using DSCC_CW1_Frontend_11733.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DSCC_CW1_Frontend_11733.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IApiService _apiService;

        public ArticleController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var articles = await _apiService.GetAllArticles();
            return View(articles);
        }

        public async Task<IActionResult> Details(int id)
        {
            var article = await _apiService.GetArticleById(id);
            return View(article);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ArticleCreationDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(string title, string topic, string content)
        {
            // Create a new article
            var article = new ArticleCreationDto
            {
                Title = title,
                TopicName = topic,
                Content = content
            };

            // Call your API to create the article
            await _apiService.CreateArticle(article);

            // Redirect back to the Index page
            return RedirectToAction(nameof(Index));
        }

        // Action method to display the edit form
        public async Task<IActionResult> Edit(int id)
        {
            var article = await _apiService.GetArticleById(id);
            if (article == null)
            {
                return NotFound();
            }

            var articleUpdateDto = new ArticleUpdateDto
            {
                Id = id,
                Title = article.Title,
                Content = article.Content,
                TopicName = article.ArticleTopic.Name
            };

            return View(articleUpdateDto);
        }

        // Action method to handle the submission of the edit form
        [HttpPost]
        public async Task<IActionResult> Edit(ArticleUpdateDto articleUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(articleUpdateDto);
            }

            // Call your API to update the article
            await _apiService.UpdateArticle(articleUpdateDto.Id, articleUpdateDto);

            // Redirect back to the list of article or details page
            return RedirectToAction(nameof(Index)); // or Details page
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(new Article());
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var article = await _apiService.GetArticleById(id);
            if (article == null)
            {
                return NotFound();
            }

            // Call your API to delete the article
            await _apiService.DeleteArticleById(id);

            // Redirect back to the list of article
            return RedirectToAction(nameof(Index));
        }
    }
}
