using GitAnalyser.Interactor.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace GitAnalyser.Interactor
{
    public static class GitInteractorModule
    {
        public static void RegisterServices(IServiceCollection collection)
        {
            collection.AddSingleton<IFileCopier, FileCopier>();
            collection.AddSingleton<IDataAnalysisFactory, DataAnalysisFactory>();
            collection.AddSingleton<IRepositoryAnalyser, RepositoryAnalyser>();
        }
    }
}
