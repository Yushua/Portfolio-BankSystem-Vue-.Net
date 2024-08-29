using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MyApp.Data
{
    public class DataContextDapper
    {
        private IConfiguration _config;
        public DataContextDapper(IConfiguration config){
            _config = config;
        }

        public IEnumerable<T>? LoadData<T>(string sql, object? parameters = null)
        {
            using IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Query<T>(sql, parameters);
        }

        public int LoadDataSingle<T>(string sql, object parameters)
        {
            using (IDbConnection dbConnection = CreateConnection())
            {
                return dbConnection.ExecuteScalar<int>(sql, parameters);
            }
        }
        
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        }

        public void ExecuteSql(string sql, object parameters)
        {
            using (IDbConnection dbConnection = CreateConnection())
            {
                dbConnection.Execute(sql, parameters);
            }
        }

        public int ExecuteSqlWithRowCount(string sql, object? parameters = null)
        {
            using IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Execute(sql, parameters);
        }
    }
}
