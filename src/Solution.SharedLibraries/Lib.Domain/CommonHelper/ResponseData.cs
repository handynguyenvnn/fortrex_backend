using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Lib.Domain.ModelApi
{
    public class ResponseData
    {
        public ResponseData()
        {
            Result = HttpStatusCode.OK;
        }

        public object Reply { get; set; }
        public bool Error {
            get {
                return this.Result == HttpStatusCode.OK ? false : true;
            }
        }
        public HttpStatusCode Result { get; set; }

        private object _Data;
        public object Data {
            get {
                if(this.Result == HttpStatusCode.OK)
                {
                    return null;
                }
                else
                {
                    return _Data;
                }
            }
            set {
                this._Data = value;
            }
        }
        public string Message { get; set; }
    }

    public class TranferResponse
    {
        public string from { get; set; }
        public string to { get; set; }
        public double amount { get; set; }
        public string hashtx { get; set; }
        public string created_on { get; set; }
    }

   
}