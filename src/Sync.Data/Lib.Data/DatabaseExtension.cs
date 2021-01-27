using System.Data;
using System.Linq;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using System.Xml;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace Lib.Core.Data
{
    public static class DatabaseExtension
    {
        public static IEnumerable<TResult> Execute<TResult>(this Database db, string storedProcedureName, IRowMapper<TResult> map, params DbParameter[] parameters)
        {
            var result = new List<TResult>();
            // using statement as shown below, which automatically closes and disposes the connection
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    if (storedProcedureName.ToLower().StartsWith("exec") || storedProcedureName.Contains("@"))
                        cmd.CommandType = CommandType.Text;
                    else
                        cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = storedProcedureName;

                    if (parameters != null)
                    {
                        foreach (DbParameter parameter in parameters)
                        {
                            cmd.Parameters.Add(parameter);
                        }
                    }

                    // using statement as shown below, which automatically closes and disposes the connection
                    using (IDataReader reader = db.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {
                            result.Add(map.MapRow(reader));
                        }
                    }
                }
            }

            return result;
        }


        public static IEnumerable<TResult> Execute<TResult>(this Database db, string storedProcedureName, IResultSetMapper<TResult> map, params DbParameter[] parameters)
        {
            var result = new List<TResult>();
            // using statement as shown below, which automatically closes and disposes the connection
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    if (storedProcedureName.ToLower().StartsWith("exec") || storedProcedureName.Contains("@"))
                        cmd.CommandType = CommandType.Text;
                    else
                        cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = storedProcedureName;
                    if (parameters != null)
                    {
                        foreach (DbParameter parameter in parameters)
                        {
                            cmd.Parameters.Add(parameter);
                        }
                    }
                    // using statement as shown below, which automatically closes and disposes the connection
                    using (IDataReader reader = db.ExecuteReader(cmd))
                    {
                        result = map.MapSet(reader).ToList();
                    }
                }
            }
            return result;
        }

        public static Int32 ExecuteNonQuery(this Database db, CommandType commandType, string commandText, params DbParameter[] parameters)
        {
            Int32 effectedCount = 0;
            using (var cnn = (SqlConnection)db.CreateConnection())
            {
                cnn.Open();
                using (DbCommand cmd = new SqlCommand(commandText, cnn))
                {
                    cmd.Parameters.AddRange(parameters);
                    cmd.CommandType = commandType;
                    effectedCount = cmd.ExecuteNonQuery();
                }
                cnn.Close();
            }
            return effectedCount;
        }

        public static DbParameter CreateParameter(this Database db, string name, object value = null, DbType type = DbType.Object,
                                                  ParameterDirection parameterDirection = ParameterDirection.Input)
        {
            DbParameter result = db.DbProviderFactory.CreateParameter();
            if (result != null)
            {
                result.ParameterName = db.BuildParameterName(name);
                {
                    if (value != null)
                    {
                        result.Value = value;
                    }
                    else
                    {
                        result.Value = DBNull.Value;
                    }
                }
                result.DbType = type;
                result.Direction = parameterDirection;
            }
            return result;
        }
    }
}