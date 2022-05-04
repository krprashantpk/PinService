using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenService.Domain.Core.Logging
{
    public class Response
    {
        public Response(long delay, int statusCode, string body,  IDictionary<string, string> headers)
        {
            StatusCode = statusCode;
            Body = body;
            Delay = delay;
            Headers = headers;
        }

        public int StatusCode { get; private set; }
        public string Body { get; private set; }
        public long Delay { get; private set; }

        public virtual IDictionary<string, string> Headers { get; private set; }
    }
}
