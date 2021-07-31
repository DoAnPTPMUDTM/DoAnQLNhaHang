using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonController : ControllerBase
    {

        private readonly ILogger _logger;

        public MonController(ILogger<MonController> logger)
        {
            _logger = logger;
        }
        [HttpPost("test")]
        public void OnGet([FromBody] List<int> lst)
        {
            foreach(int i in lst)
            {
                _logger.LogInformation(i.ToString());
            }    
        }
    }
}
