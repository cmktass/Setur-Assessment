using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exception
{
    public class BadRequestException : System.Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public BadRequestException(string message, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest) : base(message: message)
        {
            StatusCode = httpStatusCode;
        }
    }
}
