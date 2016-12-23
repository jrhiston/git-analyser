using Microsoft.AspNetCore.Mvc;
using GitAnalyser.Interactor;
using GitAnalyser.Interactor.Commands;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace git_analyser.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepositoryAnalyser _repositoryAnalyser;
        private readonly ViewRenderService _viewRenderService;

        public HomeController(
            IRepositoryAnalyser repositoryAnalyser,
            ViewRenderService viewRenderService)
        {
            _repositoryAnalyser = repositoryAnalyser;
            _viewRenderService = viewRenderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Analyse(string gitUrl)
        {
            AnalysisResults result = await _repositoryAnalyser.AnalyseAsync(
                new RepositoryUrl(gitUrl),
                new RepositoryDestination(
                    Path.Combine(
                        GitRepositoryDestinations.Default, 
                        Path.GetFileNameWithoutExtension(gitUrl))));

            return Json(result.OfType<DataAnalysisResult>().ToList());
        }
    }
}
