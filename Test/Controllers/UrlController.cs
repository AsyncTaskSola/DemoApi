using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Test.Database;
using Test.Entity;
using Test.Interface;

namespace Test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlController : ControllerBase
    {
        private readonly ILogger<UrlController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IDataInterface _dataInterface;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UrlController(ILogger<UrlController> logger, IConfiguration configuration,IDataInterface dataInterface, 
IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _configuration = configuration;
            _dataInterface = dataInterface;
            _httpContextAccessor = httpContextAccessor;
        }

        public string Host { get; set; }

        [HttpGet]
        public IActionResult GetUrl()
        {
            Host = _httpContextAccessor.HttpContext.Request.GetDisplayUrl();
            DataWays dataWays = new DataWays(_configuration);
            var table = dataWays.Select("select * from Url");
            return Ok(_dataInterface.GetAll(Host, table));
        }

        [HttpGet("{pageid}")]
        public IActionResult GetUrl(int pageid)
        {
            Host = _httpContextAccessor.HttpContext.Request.GetDisplayUrl();
            DataWays dataWays = new DataWays(_configuration);
            var table = dataWays.Select("select * from Url");
            var data = _dataInterface.GetPageSize(Host,table, pageid);
            if (data.urlEntities.Count == 0)
            {
                return NotFound();
            }
            return Ok(_dataInterface.GetPageSize(Host,table, pageid));
        }
    }
}
