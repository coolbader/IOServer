using IOServer.Logic.IGrains;
using IOServer.Logic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IOServer.Logic.Client.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LogicTestController : ControllerBase
    {
        private readonly IClusterClient _clusterClient;
        private readonly IBusinessLogicGrain _logic;

        public LogicTestController(IClusterClient clusterClient)
        {
            _clusterClient = clusterClient;
            _logic = _clusterClient.GetGrain<IBusinessLogicGrain>(Guid.NewGuid().ToString());
        }
        [HttpPost]
        public async Task<IActionResult> Register()
        {
            await _logic.Register();
            return Ok(await _logic.GetResult());
        }
        [HttpPost]
        public async Task<IActionResult> Publish([FromBody] DataPoint data)
        {
            await _logic.Register();
            return Ok(await _logic.GetResult());
        }

        [HttpPost]
        public async Task<IActionResult> StartAsync([FromBody]DataPoint data)
        {
            await _logic.StartProcess(data);
            return Ok(await _logic.GetResult());
        }
        [HttpPost]
        public async Task<IActionResult> WorkAsync([FromBody] DataPoint data)
        {
            await _logic.WorkProcess(data);
            return Ok(await _logic.GetResult());
        }
        [HttpPost]
        public async Task<IActionResult> CompletedAsync([FromBody] DataPoint data)
        {
            await _logic.CompletedProcess(data);
            return Ok(await _logic.GetResult());
        }
        [HttpPost]
        public async Task<IActionResult> ListenAsync([FromBody] DataPoint data)
        {
            await _logic.Listen(data);
            return Ok(await _logic.GetResult());
        }
        [HttpPost]
        public async Task<IActionResult> EndAsync([FromBody] DataPoint data)
        {
            await _logic.End(data);
            return Ok(await _logic.GetResult());
        }
       
    }
}
