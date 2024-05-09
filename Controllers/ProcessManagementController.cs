using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Simple_Process_Management_API.Model;
using Simple_Process_Management_API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Simple_Process_Management_API.Controllers
{
    
    [ApiVersion(1)]
    [ApiController]
    public class ProcessManagementController(ProcessManager processManager) : ControllerBase
    {
        // endpoint: api/v1/create-process
        [MapToApiVersion(1)]
        [Route("api/v{v:apiVersion}/create-process")]
        [HttpGet]
        public async Task<ActionResult<ProcessModel>> CreateProcess()
        {
            var process = await processManager.CreateProcess();
            return Ok(new { process.PID, process.CreationTime });
        }

        // endpoint: api/v1/get-single/{pid}
        [MapToApiVersion(1)]
        [Route("api/v{v:apiVersion}/get-single/{pid}")]
        [HttpGet]
        public async Task<ActionResult<List<string>>> GetSingleProcessLogs(int pid)
        {
            var process = await processManager.GetProcess(pid);
            if (process == null)
            {
                return NotFound("Process not found");
            }
            return Ok(process.Logs);
        }

        // endpoint: api/v1/get-all
        [MapToApiVersion(1)]
        [Route("api/v{v:apiVersion}/get-all")]
        [HttpGet]
        public async Task<ActionResult<List<ProcessModel>>>  GetAllProcess()
        {
            var processes = await processManager.GetAllProcesses();
            return Ok(processes.Select(p => new
            {
                p.PID,p.CreationTime
            }));
        }
        

        // endpoint: api/v1/delete-process/{pid}
        [MapToApiVersion(1)]
        [Route("api/v{v:apiVersion}/delete-process/{pid}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteProcess(int pid)
        {
            bool isValid = await processManager.DeleteProcess(pid);
            if (isValid)
                return Ok();
            else
                return NotFound();
        }
    }
}
