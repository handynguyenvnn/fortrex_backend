using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data.ResultSetMapper
{
    public class GuidResultSetMapper : IResultSetMapper<Guid>
    {
        #region IResultSetMapper<Guid> Members

        public IEnumerable<Guid> MapSet(IDataReader reader)
        {
            using (reader)
            {
                while (reader.Read())
                {
                    yield return reader.GetGuid(0);
                }
            }
        }

        #endregion
    }
}
