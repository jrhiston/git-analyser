using System.Diagnostics;
using System.IO;

namespace GitAnalyser.Interactor
{
    public class RepositoryCloner : IRepositoryCloner
    {
        private Process _gitProcess;

        public RepositoryCloner()
        {
            _gitProcess = new Process();
        }

        public string Result {get;set;}

        public IRepositoryCloner Clone(string repositoryAddress, string folderName, string path = null)
        {
            var processInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                FileName = "git.exe",
                CreateNoWindow = true,
                WorkingDirectory = (path != null && Directory.Exists(path))
                    ? path
                    : Directory.GetCurrentDirectory()
            };

            _gitProcess.StartInfo = processInfo;

            RunCommand($"clone {repositoryAddress} {folderName}");

            return this;
        }

        private void RunCommand(string args)
        {
            _gitProcess.StartInfo.Arguments = args;
            _gitProcess.Start();
            Result = _gitProcess.StandardOutput.ReadToEnd().Trim();
            _gitProcess.WaitForExit();
        }
    }
}
