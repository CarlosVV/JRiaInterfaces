using CES.Data.Sql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CES.CoreApi.Receipt_Main.Service.Repositories
{
    /// <summary>
    ///This is to register connection strings.
    ///If you don’t use main and  ReadOnlyTransactional please remove them or replace them
    /// </summary>
    public class DatabaseName
    {
        public static readonly string Main = "main";
        public static readonly string ReadOnlyTransactional = "readonlyTransactional";
        public static readonly string Transactional = "transactional";

        internal static SqlMapper CreateSqlMapper()
        {
            var sqlMapper = new SqlMapper();
            foreach (ConnectionStringSettings item in ConfigurationManager.ConnectionStrings)
            {
                sqlMapper.AddConnectionSetting(item.Name, item.ConnectionString, 30);
            }
            return sqlMapper;
        }

        internal static ConnectionStringSettings GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name];

        }

        public static string GetStoredProcedureExecutionLogText(string spName, ICollection<SqlParameter> paramCollect)
        {
            var log = String.Empty;
            var fullExec = "";
            var sqlCmd = "exec " + spName + " ";
            var declareStmt = "";
            var paramList = "";

            try
            {
                IEnumerator e = paramCollect.GetEnumerator();
                while (e.MoveNext())
                {
                    SqlParameter p = (SqlParameter)e.Current;
                    if (p.Direction == ParameterDirection.Output)
                    {
                        if (p.DbType == DbType.Int32)
                        {
                            declareStmt += "declare " + p + " int|";
                        }
                        else if (p.DbType == DbType.String || p.DbType == DbType.AnsiString)
                        {
                            declareStmt += "declare " + p + " varchar(100)|";
                        }
                        else if (p.DbType == DbType.DateTime)
                        {
                            declareStmt += "declare " + p + " datetime|";
                        }
                        else if (p.DbType == DbType.Boolean)
                        {
                            declareStmt += "declare " + p + " bit|";
                        }
                        else
                        {
                            declareStmt += "declare " + p + " " + p.DbType + "|";
                        }
                    }
                }

                e = paramCollect.GetEnumerator();

                while (e.MoveNext())
                {
                    SqlParameter p = (SqlParameter)e.Current;
                    if (p.Direction == ParameterDirection.Output)
                    {
                        paramList += p + " = " + p + " output,";
                    }
                    else if (p.DbType == DbType.String
                        || p.DbType == DbType.AnsiString
                        || p.DbType == DbType.DateTime
                        || p.DbType == DbType.Boolean)
                    {
                        paramList += p + " = '" + p.Value + "',";
                    }
                    else
                    {
                        paramList += p + " = " + (p.Value ?? "null") + ",";
                    }
                }

                fullExec = declareStmt + sqlCmd + paramList.Remove(paramList.LastIndexOf(","), 1);
                log = fullExec.Replace("|", Environment.NewLine);
            }
            catch (Exception e)
            {
                log = "Cannot parse and write out SP call logging." + e.Message;
            }

            return log;
        }

    }
}