using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonController : ControllerBase
    {
        QuanLyNhaHangContext db;
        //private readonly ILogger _logger;

        //public MonController(ILogger<MonController> logger)
        //{
        //    _logger = logger;
        //}
        //[HttpPost("test")]
        //public void OnGet([FromBody] List<int> lst)
        //{
        //    foreach(int i in lst)
        //    {
        //        _logger.LogInformation(i.ToString());
        //    }    
        //}
        //
        public MonController(QuanLyNhaHangContext db)
        {
            this.db = db;
        }
        [HttpGet("getallmon")]
        public JsonResult getallmon()
        {
            var mons = (from mon in db.Mons
                        select new
                        {
                            MaMon = mon.MaMon,
                            TenMon = mon.TenMon,
                            Anh = mon.Anh,
                            GiaGoc = mon.GiaGoc,
                            GiaKM = mon.GiaKm
                        }).ToList();
            return new JsonResult(mons);
        }
        [HttpGet("getmonbymanhom/{manhom}")]
        public JsonResult getmonbymanhom(int manhom)
        {
            var mons = (from mon in db.Mons
                        where mon.MaNhom == manhom
                        select new
                        {
                            MaMon = mon.MaMon,
                            TenMon = mon.TenMon,
                            Anh = mon.Anh,
                            GiaGoc = mon.GiaGoc,
                            GiaKM = mon.GiaKm
                        }).ToList();
            return new JsonResult(mons);
        }
        [HttpGet("search")]
        public JsonResult search([FromQuery] string tukhoa)
        {
            var mons = (from mon in db.Mons
                        select new
                        {
                            MaMon = mon.MaMon,
                            TenMon = mon.TenMon,
                            Anh = mon.Anh,
                            GiaGoc = mon.GiaGoc,
                            GiaKM = mon.GiaKm
                        }).Where(t=>t.TenMon.Contains(tukhoa)).ToList();
            return new JsonResult(mons);
        }

    }
}
