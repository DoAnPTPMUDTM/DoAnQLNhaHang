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
    public class NguoiDungController : ControllerBase
    {
        QuanLyNhaHangContext db;
        public NguoiDungController(QuanLyNhaHangContext db)
        {
            this.db = db;
        }
        [HttpPost("login")]
        public JsonResult login([FromQuery] string taikhoan, [FromQuery] string matkhau)
        {
            NguoiDung nd = db.NguoiDungs.Where(t => t.TenDn == taikhoan && t.MatKhau == matkhau).FirstOrDefault();
            if(nd== null)
            {
                return new JsonResult(new ResultRequest("0", "Đăng nhập thất bại"));
            }
            return new JsonResult(new ResultRequest("1", "Đăng nhập thành công"));
        }
    }

}
