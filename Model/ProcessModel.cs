using Newtonsoft.Json;

namespace Simple_Process_Management_API.Model
{
    public class ProcessModel
    {
        [JsonProperty("PID")]
        public int PID { get; set; }
        [JsonProperty("Time now")]
        public DateTime TimeNow { get; set; }
        [JsonProperty("Creation time")]
        public string CreationTime => TimeNow.ToString("hh:mm tt dd.MM.yyyy");
        [JsonProperty("Logs")]
        public List<string> Logs { get; set; } = new List<string>();
        public required CancellationTokenSource CancelTokenSource { get; set; }
    }
}
