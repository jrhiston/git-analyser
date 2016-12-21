using Microsoft.AspNetCore.Mvc;
using GitAnalyser.Interactor;
using System;

namespace git_analyser.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepositoryAnalyser _repositoryAnalyser;

        public HomeController(IRepositoryAnalyser repositoryAnalyser)
        {
            _repositoryAnalyser = repositoryAnalyser;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AnalyzeGitRepository(string gitUrl)
        {
            var folder = Guid.NewGuid();

            var result = _repositoryAnalyser.Analyse(
                new RepositoryUrl(gitUrl),
                new RepositoryDestination("~/git_repos/", folder.ToString()));

            return Json(result);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
