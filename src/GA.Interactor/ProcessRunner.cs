using System.Diagnostics;
using System.IO;

namespace GitAnalyser.Interactor
{
    internal static class ProcessRunner
    {
        public static string RunGitCommand(string args = null)
        {
            var processInfo = new ProcessStartInfo()
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                FileName = "git.exe",
                CreateNoWindow = true
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

        public static string RunCommand(string directoryPath, string file)
        {
            var processInfo = new ProcessStartInfo("cmd.exe", "/c " + file)
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                WorkingDirectory = directoryPath
            };

            var _gitProcess = new Process();
            _gitProcess.StartInfo = processInfo;

            _gitProcess.Start();
            string output = _gitProcess.StandardOutput.ReadToEnd().Trim();
            _gitProcess.WaitForExit();
            return output;
        }
    }
}
