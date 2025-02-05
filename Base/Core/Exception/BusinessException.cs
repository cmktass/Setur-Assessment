using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exception
{
    public class BusinessException : System.Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public BusinessException(
            string message,
            HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
