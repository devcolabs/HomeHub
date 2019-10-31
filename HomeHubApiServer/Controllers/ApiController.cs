using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeHubApiServer.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeHubApiServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private HubService _hubService;

        public ApiController(HubService hubService)
        {
            _hubService = hubService;
        }

        // Command request 
        [HttpGet("command/{address}/{command}/{data}")]
        public IActionResult ProcessCommand(string address,string command, string data)
        {
            try
            {
                var ok = _hubService.ProcessCommand(address, command, data);
                return new JsonResult( new { ok, address, command, data });
            }
            catch(Exception ex)
            {
                return BadRequest(new JsonResult(new { ok = false, text = ex.Message }));
            }
                
        }

        // Group command request
        [HttpGet("group/{group}/{command}/{data}")]
        public IActionResult  ProcessGroupCommand(string group, string command, string data)
        {
            try
            {
                var ok = _hubService.ProcessGroup(group, command, data);
                return new JsonResult(new { ok, group, command, data }); 
            }
            catch(Exception ex)
            {
                return BadRequest(new JsonResult(new { ok = false, text = ex.Message }));
            }
        }

        // Status request
        [HttpGet("status/{address}")]
        public IActionResult  GetStatus(string address)
        {
            try
            {
                var level = _hubService.ProcessStatus(address);
                var ok = level >= 0;
                return new JsonResult( new { ok , level }); 
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResult(new { ok = false, text = ex.Message }));
            }
        }
    }
}
