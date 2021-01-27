using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data.ResultSetMapper
{
    public class LongResultSetMapper : IResultSetMapper<long>
    {
        public IEnumerable<long> MapSet(IDataReader reader)
        {
            using (reader)
            {
                while (reader.Read())
                {
                    yield return reader.GetInt64(0);
                }
            }
        }

    }
}
