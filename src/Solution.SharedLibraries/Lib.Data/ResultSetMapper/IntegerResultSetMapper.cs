using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Lib.Data.ResultSetMapper
{
    public class IntegerResultSetMapper : IResultSetMapper<int>
    {
        #region IResultSetMapper<int> Members

        public IEnumerable<int> MapSet(IDataReader reader)
        {
            using (reader)
            {
                while (reader.Read())
                {
                    yield return reader.GetInt32(0);
                }
            }
        }

        #endregion
    }
}
