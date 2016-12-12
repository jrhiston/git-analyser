using System.Diagnostics;
using System.IO;

namespace GitAnalyser.Interactor
{
    public static class ProcessRunner
    {
        public static string RunCommand(string directoryPath, string file, string args = null)
        {
            var processInfo = new ProcessStartInfo(Path.Combine(directoryPath, file))
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                WorkingDirectory = directoryPath
            };

            var _gitProcess = new Process();
            _gitProcess.StartInfo = processInfo;

            if (args != null)
            {
                _gitProcess.StartInfo.Arguments = args;
            }

            _gitProcess.Start();
            string output = _gitProcess.StandardOutput.ReadToEnd().Trim();
            _gitProcess.WaitForExit();
            return output;
        }
    }
}
