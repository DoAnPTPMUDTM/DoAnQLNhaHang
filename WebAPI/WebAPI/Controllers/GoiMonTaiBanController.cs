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
    public class GoiMonTaiBanController : ControllerBase
    {
        QuanLyNhaHangContext db;
        public GoiMonTaiBanController(QuanLyNhaHangContext db)
        {
            this.db = db;
        }
        [HttpPost("insert/{mahd}")]
        public JsonResult insert(GoiMonTaiBan goiMonTaiBan, int mahd)
        {
            if(goiMonTaiBan != null)
            {
                GoiMonTaiBan gmtb = new GoiMonTaiBan();
                gmtb.MaHd = mahd;
                gmtb.MaMon = goiMonTaiBan.MaMon;
                gmtb.SoLuong = goiMonTaiBan.SoLuong;
                gmtb.TinhTrang = 0;
                if(goiMonTaiBan.GhiChu == null)
                {
                    gmtb.GhiChu = "Không";
                }
                else
                {
                    gmtb.GhiChu = goiMonTaiBan.GhiChu;
                }
                db.GoiMonTaiBans.Add(gmtb);
                db.SaveChanges();
                return new JsonResult(new ResultRequest("1", "Thành công"));
            }
            return new JsonResult(new ResultRequest("0", "Thất bại"));
        }
    }
}
