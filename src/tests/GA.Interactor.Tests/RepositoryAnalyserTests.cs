using GitAnalyser.Interactor;
using NUnit.Framework;
using System;

namespace GA.Interactor.Tests
{
    [TestFixture]
    public class RepositoryAnalyserTests
    {
        [Test]
        public void MethodAnalyse_ReturnsLogs_GivenPath()
        {
            var sut = new RepositoryAnalyser(new RepositoryCloner(), new FileCopier());

            var logs = sut.Analyse(
                @"G:\Web Projects\downloads", 
                "https://github.com/jrhiston/git-analyser.git",
                Guid.NewGuid().ToString());

            Assert.That(logs, Is.Not.Empty);
        }
    }
}
