using git_analyser.Controllers;
using GitAnalyser.Interactor;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace git_analyser.tests
{
    public class HomeControllerTests
    {
        [Fact]
        public async void MethodAnalyse_GitUrlGiven_GitRepositoryAnalysed()
        {
            var sut = new HomeController(new RepositoryAnalyser(new FileCopier()));

            JsonResult result = await sut.Analyse("https://github.com/jrhiston/git-analyser.git");

            Assert.NotNull(result);
        }
    }
}
