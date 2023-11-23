using System.Diagnostics;

namespace ExecService
{
    public class Service
    {
        private readonly ILogger<WindowsBackgroundService> logger;

        public Service(ILogger<WindowsBackgroundService> logger)
        {
            this.logger = logger;
        }

        public async Task Execute(ExecCommand execCommand)
        {
            try
            {
                await ConsoleExecute(execCommand);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Error ExecCommand");
            }
        }

        public async Task ConsoleExecute(ExecCommand itemCommand)
        {
            var processStartInfo = new ProcessStartInfo(itemCommand.Parameter1, itemCommand.Parameter2)
            {
                CreateNoWindow = itemCommand.CreateNoWindow,
                UseShellExecute = itemCommand.UseShellExecute,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WorkingDirectory = itemCommand.Parameter3
            };

            var output = new List<string>();

            var process = Process.Start(processStartInfo);

            if (process != null)
            {
                process.Start();

                if (itemCommand.WaitForExit)
                {
                    process.OutputDataReceived += (sender, args1) => {
                        if (args1.Data != null)
                        {
                            output.Add(args1.Data);
                        }
                    };

                    process.BeginOutputReadLine();
                    process.WaitForExit();
                }

                await Task.Delay(itemCommand.Delay);

                logger.LogInformation("ExecCommand, Console run successfully");

                logger.LogInformation("ExecCommand, result: {0}", string.Join(Environment.NewLine, output));
            }
        }
    }
}
