using GitAnalyser.Interactor.Commands;

namespace GitAnalyser.Interactor
{
    internal interface IPipelineFactory
    {
        DataAnalysisPipeline CreateDataAnalysisPipeline(
            RepositoryUrl repositoryUrl,
            RepositoryDestination repositoryDestination);
    }
}