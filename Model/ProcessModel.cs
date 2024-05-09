using System.Collections.Concurrent;

namespace Simple_Process_Management_API.Model
{
    public class ProcessModel
    {
        public int PID { get; set; }
        public DateTime CreationTime { get; set; }
        public string FormattedCreationTime => CreationTime.ToString("hh:mm tt dd.MM.yyyy");
        public List<string> Logs { get; set; } = new List<string>();
        public required CancellationTokenSource CancelTokenSource { get; set; }
    }
}
