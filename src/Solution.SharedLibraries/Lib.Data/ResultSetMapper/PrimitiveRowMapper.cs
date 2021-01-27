using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Lib.Data.ResultSetMapper
{
    public class PrimitiveRowMapper<T> : IRowMapper<T>
    {
        public T MapRow(IDataRecord row)
        {
            return (T)row[0];
        }
    }
}