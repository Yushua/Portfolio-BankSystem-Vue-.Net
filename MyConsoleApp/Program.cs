// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks.Dataflow;
using Dapper;
using Microsoft.Data.SqlClient;
using MyApp.Models;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        //main is always written and run on COnsole.App
        static void Main(string[] args)
        {

            string connectionString = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;";
            
            IDbConnection dbConnection = new SqlConnection(connectionString);

            string sqlCommand = "SELECT GETDATE()";

            DateTime rightnow = dbConnection.QuerySingle<DateTime>(sqlCommand);
            Console.WriteLine("----------------");
            Console.WriteLine(rightnow);
            Console.WriteLine("----------------");
            Computer myComputer = new Computer();
            myComputer.SetMotherboard("new");
            myComputer.SetVideoCard("cardd");
            Console.WriteLine(myComputer.GetVideoCard());
            Console.WriteLine("----------------");
        }

    }
}