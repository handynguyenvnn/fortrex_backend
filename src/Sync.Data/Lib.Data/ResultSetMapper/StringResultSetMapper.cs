using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Lib.Data.ResultSetMapper
{
    public class StringResultSetMapper : IResultSetMapper<string>
    {
        #region IResultSetMapper<int> Members

        public IEnumerable<string> MapSet(IDataReader reader)
        {
            using (reader)
            {
                while (reader.Read())
                {
                    yield return reader.GetString(0);
                }
            }
        }

        #endregion
    }
}
