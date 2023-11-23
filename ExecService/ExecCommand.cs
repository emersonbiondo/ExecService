namespace ExecService
{
    public class ExecCommand
    {
        public int Delay { get; set; }

        public string Parameter1 { get; set; }

        public string Parameter2 { get; set; }

        public string Parameter3 { get; set; }

        public bool UseShellExecute { get; set; }

        public bool CreateNoWindow { get; set; }

        public bool WaitForExit { get; set; }

        public ExecCommand()
        {
            Delay = 1;
            Parameter1 = string.Empty;
            Parameter2 = string.Empty;
            Parameter3 = string.Empty;
            UseShellExecute = true;
            CreateNoWindow = true;
            WaitForExit = true;
        }
    }
}
