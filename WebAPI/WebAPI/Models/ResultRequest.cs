using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class ResultRequest
    {
        public string Result { get; set; }
        public string Message { get; set; }
        public ResultRequest()
        {

        }
        public ResultRequest(string result, string message)
        {
            Result = result;
            Message = message;
        }
    }
}
