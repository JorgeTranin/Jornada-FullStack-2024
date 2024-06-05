using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Financas.core.Responses
{
    public class Response<TData>
    {
        [JsonConstructor]
        public Response() => _code = Configuration.DefaultStatusCode;
        public Response(TData? data, int code = 200, string? message = null)
        {
            Data = data;
            _code = code;
            Message = message;   
        }
        private int _code = Configuration.DefaultStatusCode;
        public TData? Data { get; set; }
        public string? Message { get; set; }

        [JsonIgnore]
        public bool IsSuccess => _code is >= 200 and <= 299;
    }
}