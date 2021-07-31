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
    public class BanController : ControllerBase
    {
        QuanLyNhaHangContext db;
        public BanController(QuanLyNhaHangContext db)
        {
            this.db = db;
        }
        [HttpGet("getallban")]
        public JsonResult getallban()
        {
            var bans = (from ban in db.Bans
                        select new
                        {
                            MaBan = ban.MaBan,
                            TenBan = ban.TenBan
                        }).ToList();
            return new JsonResult(bans);
        }
        [HttpGet("getttbanbymaban/{maban}")]
        public JsonResult getttbanbymaban(int maban)
        {
            Ban ban = db.Bans.Where(t => t.MaBan == maban).FirstOrDefault();
            if(ban == null)
            {
                return new JsonResult(new ResultRequest("-1", "Không tìm thấy tt bàn"));
            }
            return new JsonResult(new ResultRequest("1", ban.TrangThai.ToString()));
        }
    }
}
