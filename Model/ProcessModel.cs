namespace Simple_Process_Management_API.Model
{
    public class ProcessModel
    {
        public int ProcessId { get; set; }
        public DateTime CreationTime { get; set; }
        public List<string> Logs { get; set; } = new List<string>();
    }
}
