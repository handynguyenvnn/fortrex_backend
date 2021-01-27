using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace Web.SourceCoin.Models
{
    public class DataResponse
    {
        public DataResponse()
        {
            StatusCode = HttpStatusCode.OK;
        }
        public HttpStatusCode StatusCode { get; set; }
        public string Meg { get; set;}
        public object Reply { get; set; }
        public object Optional { get; set; }

    }
}