using GitAnalyser.Interactor.Commands;
using System.Threading.Tasks;

namespace GitAnalyser.Interactor
{
    public interface IRepositoryAnalyser
    {
        AnalysisResults Analyse(
            RepositoryUrl repository,
            RepositoryDestination repositoryDestination);

        Task<AnalysisResults> AnalyseAsync(
            RepositoryUrl repository,
            RepositoryDestination repositoryDestination);
    }
}