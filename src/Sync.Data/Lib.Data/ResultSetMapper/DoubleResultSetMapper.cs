using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data.ResultSetMapper
{
   public class DoubleResultSetMapper : IResultSetMapper<double>
    {
        public IEnumerable<double> MapSet(IDataReader reader)
        {
            using (reader)
            {
                while (reader.Read())
                {
                    yield return reader.GetDouble(0);
                }
            }
        }

    }
}
