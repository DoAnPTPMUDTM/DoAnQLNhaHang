using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhomMonController : ControllerBase
    {
        QuanLyNhaHangContext db;
        public NhomMonController(QuanLyNhaHangContext db)
        {
            this.db = db;
        }
        [HttpGet("getallnhommon")]
        public JsonResult getallnhommon()
        {
            var nhommons = (from nm in db.NhomMons
                            select new
                            {
                                MaNhom = nm.MaNhom,
                                TenNhom = nm.TenNhom
                            }).ToList();
            return new JsonResult(nhommons);
        }
    }
}
