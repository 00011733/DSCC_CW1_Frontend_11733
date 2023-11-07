using DSCC_CW1_Frontend_11733.Models;
using DSCC_CW1_Frontend_11733.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.X86;

namespace DSCC_CW1_Frontend_11733.Controllers
{
    public class TopicController : Controller
    {
        private readonly IApiService _apiService;

        public TopicController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var topics = await _apiService.GetAllTopics();
            return View(topics);
        }

        public async Task<IActionResult> Details(int id)
        {
            var topic = await _apiService.GetTopicById(id);
            return View(topic);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new TopicCreationDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            // Create a new topic
            var topic = new Topic
            {
                Name = name
            };

            // Call your API to create the topic
            await _apiService.CreateTopic(topic);

            // Redirect back to the Index page
            return RedirectToAction(nameof(Index));
        }

        // Action method to display the edit form
        public async Task<IActionResult> Edit(int id)
        {
            var topic = await _apiService.GetTopicById(id);
            if (topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }

        // Action method to handle the submission of the edit form
        [HttpPost]
        public async Task<IActionResult> Edit(Topic topic)
        {
            if (!ModelState.IsValid)
            {
                return View(topic);
            }

            // Call your API to update the topic
            await _apiService.UpdateTopic(topic);

            // Redirect back to the list of topics or details page
            return RedirectToAction(nameof(Index)); // or Details page
        }

        public async Task<IActionResult> Articles(int id)
        {
            var articles = await _apiService.GetArticlesByTopicId(id);
            return View(articles);
        }

        // Add this action method to handle topic deletion
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var topic = await _apiService.GetTopicById(id);
            if (topic == null)
            {
                return NotFound();
            }

            // Call your API to delete the topic and related articles
            await _apiService.DeleteTopicById(id);

            // Redirect back to the list of topics
            return RedirectToAction(nameof(Index));
        }
    }
}
