using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Lib.Data.ResultSetMapper
{
    public class BooleanResultSetMapper : IResultSetMapper<bool>
    {
        #region IResultSetMapper<bool> Members

        public IEnumerable<bool> MapSet(IDataReader reader)
        {
            using (reader)
            {
                while (reader.Read())
                {
                    yield return reader.GetBoolean(0);
                }
            }
        }

        #endregion
    }
}
