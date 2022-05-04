using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenService.Domain.Core.Logging
{
    public class RequestResponse
    {
        public Request? Request { get; set; }
        public Response? Response { get; set; }
    }
}
