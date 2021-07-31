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
    public class HoaDonController : ControllerBase
    {
        QuanLyNhaHangContext db;
        public HoaDonController(QuanLyNhaHangContext db)
        {
            this.db = db;
        }
        [HttpGet("getmahdbymb/{maban}")]
        public JsonResult getmahdbymb(int maban)
        {
            HoaDon hd = db.HoaDons.Where(t => t.MaBan == maban).FirstOrDefault();
            if(hd == null)
            {
                return new JsonResult(new ResultRequest("-1", "Không tìm thấy mã HD"));
            }
            return new JsonResult(new ResultRequest("1", hd.MaHd.ToString()));
        }
    }
}
