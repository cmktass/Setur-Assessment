using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core.Web
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string Error { get; set; }
        public int StatusCode { get; set; }
        public ApiResponse() { }

        public ApiResponse<T> Success(T data, int statusCode = 200)
        {

            StatusCode = statusCode;
            IsSuccess = true;
            Data = data;
            return this;
        }
        public ApiResponse<T> Fail(string error, int statusCode = 400)
        {
            IsSuccess = false;
            Data = default;
            Error = error;
            StatusCode = statusCode;
            return this;
        }
        public ApiResponse<T> Success(int statusCode = 204)
        {

            StatusCode = statusCode;
            IsSuccess = true;
            return this;
        }
    }
}
