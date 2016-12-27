using GitAnalyser.Interactor.Commands;
using GitAnalyser.Interactor.Pipes;

namespace GitAnalyser.Interactor
{
    internal interface IPipelineFactory
    {
        IPipeline<AnalysisResults> CreateDataAnalysisPipeline(
            RepositoryUrl repositoryUrl,
            RepositoryDestination repositoryDestination);
    }
}