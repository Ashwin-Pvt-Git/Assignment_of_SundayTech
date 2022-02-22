using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_of_SundayTech.Repository
{
    public class DapperRepo : IDapperInterface
    {
        private readonly IConfiguration _objConfig;
        private readonly string connectionString = "DefaultConnection";

        public DapperRepo(IConfiguration config)
        {
            _objConfig = config;
        }

        public DbConnection GetDbconnection()
        {
            return new SqlConnection(_objConfig.GetConnectionString(connectionString));
        }

        public async Task<object> Execute<T>(string strQuery, DynamicParameters objInput, CommandType commandType = CommandType.StoredProcedure, bool isSingleRecord = false)
        {
            using IDbConnection dbConnection = GetDbconnection();
            try
            {
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }
                using var objTransaction = dbConnection.BeginTransaction();
                try
                {
                    var objOutput = dbConnection.Query<T>(strQuery, objInput, commandType: commandType, transaction: objTransaction);
                    objTransaction.Commit();
                    if (isSingleRecord)
                    {
                        return objOutput.AsEnumerable().FirstOrDefault();
                    }
                    else
                    {
                        return objOutput.AsEnumerable().ToList();
                    }
                }
                catch (Exception ex)
                {
                    objTransaction.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }
        }
    }

}
