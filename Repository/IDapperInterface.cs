using Dapper;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Assignment_of_SundayTech.Repository
{
    public interface IDapperInterface
    {
        DbConnection GetDbconnection();
        Task<object> Execute<T>(string strQuery, DynamicParameters objInput, CommandType commandType = CommandType.StoredProcedure, bool isSingleRecord = false);
    }
}
