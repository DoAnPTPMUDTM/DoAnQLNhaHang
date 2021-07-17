using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Web;
using System.Net;

namespace BLLDAL
{
    public class Mail
    {
        public void sendMail(string Email, string matKhau, string hoTen)
        {
            string to = Email;
            string from = "noreplykrt@gmail.com";
            string password = "VyMai1102";
            MailMessage message = new MailMessage();
            message.To.Add(to);
            message.From = new MailAddress(from);
            message.Subject = MailHelper.titleMail;
            message.Body = MailHelper.createMessage(matKhau, hoTen);
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(from, password);
            try
            {
                smtp.Send(message);
            }
            catch
            {

            }
        }
        public class MailHelper
        {
            public static string titleMail = "";
            public static string createMessage(string pass, string name)
            {
                string message = "Xin chào. ";
                message += name;
                message += "\n";
                message += "\n";
                message += "Mật khẩu đăng nhập vào phần mềm Quản lý nhà hàng của bạn là :";
                message += pass;
                message += "\n";
                message += "Hãy bảo vệ tài khoản của bạn!";
                message += "\n";
                message += "\n";
                message += "Trân trọng cảm ơn!";
                return message;
            }
        }
    }
}
