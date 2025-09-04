using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Response
{
    public class BaseResponse
    {
        public int code { get; set; }
        public string? message { get; set; }
        public object? data { get; set; }
        public BaseResponse()
        {
        }
        public BaseResponse(object data)
        {
            this.code = 200;
            this.message = "success";
            this.data = data;
        }
        public BaseResponse(int code, string message)
        {
            this.code = code;
            this.message = message;
            this.data = null;
        }
        public BaseResponse(int code, object data)
        {
            this.code = code;
            this.message = "success";
            this.data = data;
        }
        public BaseResponse(int code, string message, object data)
        {
            this.code = code;
            this.message = message;
            this.data = data;
        }
    }
    public class BaseResponse<T>
    {
        public int code { get; set; }
        public string message { get; set; }
        public T? data { get; set; }
        public BaseResponse(int code, string message, T data)
        {
            this.code = code;
            this.message = message;
            this.data = data;
        }
        public BaseResponse(int code, T data)
        {
            this.code = code;
            this.message = "success";
            this.data = data;
        }
        public BaseResponse(int code, string message)
        {
            this.code = code;
            this.message = message;
            this.data = default(T);
        }
    }
}
