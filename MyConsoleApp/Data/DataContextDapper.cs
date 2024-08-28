using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace MyApp.Data
{
    public class DataContextDapper
    {
        private string _connectionString = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;";

        public IEnumerable<T>? LoadData<T>(string sql, object? parameters = null)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Query<T>(sql, parameters);
        }

        public T LoadDataSingle<T>(string sql, object? parameters = null)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.QuerySingleOrDefault<T>(sql, parameters);
        }

        public bool ExecuteSql(string sql, object? parameters = null)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            return (dbConnection.Execute(sql, parameters) > 0);
        }

        public int ExecuteSqlWithRowCount(string sql, object? parameters = null)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Execute(sql, parameters);
        }
    }
}
