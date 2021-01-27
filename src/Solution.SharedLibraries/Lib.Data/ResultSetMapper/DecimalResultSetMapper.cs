using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data.ResultSetMapper
{
    public class DecimalResultSetMapper : IResultSetMapper<decimal>
    {
        #region IDecimalResultSetMapper<int> Members

        public IEnumerable<decimal> MapSet(IDataReader reader)
        {
            using (reader)
            {
                while (reader.Read())
                {
                    yield return reader.GetDecimal(0);
                }
            }
        }

        #endregion
    }
}
