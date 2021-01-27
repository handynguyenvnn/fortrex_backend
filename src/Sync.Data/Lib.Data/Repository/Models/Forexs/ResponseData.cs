using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data.Repository.Models.Forexs
{
    public class ResponseForexData
    {
        public List<DataForex> Response { get; set; }
    }
    public class DataForex
    {
        public decimal b { get; set; }
        public decimal a { get; set; }
        public decimal p { get; set; }
        public string s { get; set; }
    }
    public class ResponseData
    {
        public bool Status { get; set; }
        public int Code { get; set; }
        public string Msg { get; set; }
        public List<Datas> Response { get; set; }
    }

    public class Datas
    {
        public decimal O { get; set; }
        public decimal H { get; set; }
        public decimal L { get; set; }
        public decimal C { get; set; }
        public UInt64 T { get; set; }
        public string Tm { get; set; }
        public string symbol { get; set; }
    }

    public class ResponseLatest
    {
        public bool Status { get; set; }
        public int Code { get; set; }
        public string Msg { get; set; }
        public List<DataLatest> Response { get; set; }
    }

    public class DataLatest
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal Change { get; set; }
        public decimal Chg_perC { get; set; }
        public string Last_changed { get; set; }
        public string Symbol { get; set; }
    }
}
