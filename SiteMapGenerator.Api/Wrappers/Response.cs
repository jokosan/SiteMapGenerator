using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteMapGenerator.Api.Wrappers
{
    public class Response<T>
    {
        public Response()
        { 
        }

        public Response(T data)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }

        public Response(IEnumerable<T> data)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            DataList = data;
        }

        public IEnumerable<T> DataList { get; set; }
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }
    }
}
