using Simple_Process_Management_API.Model;

namespace Simple_Process_Management_API.Services;

public class ProcessManager
{
    private readonly Dictionary<int, ProcessModel> _processes = new Dictionary<int, ProcessModel>();
    private int _nextId = 43217;
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(5);

    public async Task<ProcessModel> CreateProcess()
    {
        _nextId += 7;
        int processId = _nextId;

        var cancelTokenSource = new CancellationTokenSource();
        var processInfo = new ProcessModel
        {
            PID = processId,
            CreationTime = DateTime.UtcNow.ToLocalTime(),
            CancelTokenSource = cancelTokenSource
        };

        await Task.Factory.StartNew(async () =>
        {
            try
            {
                while (!cancelTokenSource.Token.IsCancellationRequested)
                {
                    await Task.Delay(TimeSpan.FromSeconds(20), cancelTokenSource.Token);
                    processInfo.Logs.Add($"{DateTime.UtcNow.ToLocalTime():hh:mm tt dd.MM.yyyy}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }, cancelTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

        _processes.Add(processId, processInfo);
        _semaphore.Release();

        return processInfo;
    }

    public async Task<ProcessModel?> GetProcess(int processId)
    {
        await _semaphore.WaitAsync();
        try
        {
            _processes.TryGetValue(processId, out var processModel);
            return processModel;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<List<ProcessModel>> GetAllProcesses()
    {
        await _semaphore.WaitAsync();
        try
        {
            return _processes.Values.ToList();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<bool> DeleteProcess(int processId)
    {
        await _semaphore.WaitAsync();
        try
        {
            if (_processes.TryGetValue(processId, out var processModel))
            {
                processModel.CancelTokenSource.Cancel();
                return _processes.Remove(processId);
            }

            return false;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}