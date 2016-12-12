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

        public string Clone(string repositoryAddress, string folderName, string path = null)
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

            return RunCommand($"clone {repositoryAddress} {folderName}");
        }

        private string RunCommand(string args)
        {
            _gitProcess.StartInfo.Arguments = args;
            _gitProcess.Start();
            string output = _gitProcess.StandardOutput.ReadToEnd().Trim();
            _gitProcess.WaitForExit();
            return output;
        }
    }
}
