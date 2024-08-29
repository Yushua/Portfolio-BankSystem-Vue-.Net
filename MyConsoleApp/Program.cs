using System;
using System.Collections.Generic;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyApp.Data;
using MyApp.Models;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appSettings.json")
                .Build();

            DataContextDapper dapper = new DataContextDapper(config);
            DataContextEF entityFramework = new DataContextEF(config);

            ft_MyComputer(dapper, entityFramework);
        }

        static void ft_MyComputer(DataContextDapper dapper, DataContextEF entityFramework){
            Computer myComputer = new Computer
            {
                Motherboard = "HIghter4",
                VideoCard = "new VideoCard",
                CpuCores = 5,
                HasWifi = true,
                ReleaseDate = DateTime.Now, // Directly using DateTime
                Price = 933.90m
            };

            string checkSql = @"SELECT COUNT(*)
                                FROM TutorialAppSchema.Computer
                                WHERE Motherboard = @Motherboard";

            // Define the SQL insert command with parameterized values
                string insertSql = @"INSERT INTO TutorialAppSchema.Computer (
                    ComputerId,
                    Motherboard,
                    VideoCard,
                    CPUCores,
                    HasWifi,
                    ReleaseDate,
                    Price
                ) VALUES (
                    @ComputerId,
                    @Motherboard,
                    @VideoCard,
                    @CPUCores,
                    @HasWifi,
                    @ReleaseDate,
                    @Price)";

                string sqlSelect = @"SELECT
                ComputerId,
                Motherboard,
                VideoCard,
                CPUCores,
                HasWifi,
                ReleaseDate,
                Price
            FROM TutorialAppSchema.Computer";

            /*
                technically, it now all depends on what I want to do, do I want to patch
                a single value? do want to return everything? do I want to gain something, change something?
                do I want to check something?

                this can all be done with the format above WITH that statement. creating, adding, selecting, etc
            */

            // InsertSql(dapper, myComputer, checkSql, insertSql);
            // SelectSql(dapper, myComputer, checkSql);

            // InsertEF(entityFramework, myComputer);
            // SelectEF(entityFramework);

            // ft_WriteFile(myComputer, insertSql, "all", "log.txt", true);
            // ft_WriteFile(myComputer, insertSql, "stream", "log.txt", true);
            // ft_WriteFile(myComputer, insertSql, "stream", "log.txt", true);

            // Console.WriteLine(ft_ReadFile("log.txt"));
        }

        static void ft_WriteFile(Computer myComputer, string insertSql, string type, string fileName, bool newLine){   
            string contentToWrite = newLine ? Environment.NewLine + insertSql + Environment.NewLine : insertSql;
            if (type == "all"){
                File.WriteAllText(fileName, contentToWrite);
            }
            else if (type == "stream"){
                using StreamWriter openFile = new(fileName, append: true);
                openFile.WriteLine(contentToWrite);
                openFile.Close();
            }
        }

        static string ft_ReadFile(string fileName){
            return File.ReadAllText(fileName);
        }

        static void InsertSql(DataContextDapper dapper,
        Computer myComputer,
        string checkSql,
        string insertSql){
            int count = dapper.LoadDataSingle<int>(checkSql, new { Motherboard = myComputer.Motherboard });

            if (count > 0)
            {
                Console.WriteLine("The record already exists.");
            }
            else
            {
                // Directly use DateTime without conversion
                dapper.ExecuteSql(insertSql, new
                {
                    ComputerId = myComputer.ComputerId,
                    Motherboard = myComputer.Motherboard,
                    VideoCard = myComputer.VideoCard,
                    CPUCores = myComputer.CpuCores,
                    HasWifi = myComputer.HasWifi,
                    ReleaseDate = myComputer.ReleaseDate,
                    Price = myComputer.Price
                });

                Console.WriteLine("Record Inserted Successfully");
            }
        }

        static void InsertEF(DataContextEF entityFramework, Computer myComputer){
            bool exists = entityFramework.Computers
                .Any(c => c.Motherboard == myComputer.Motherboard);

            if (exists)
            {
                Console.WriteLine("The record already exists.");
            }
            else
            {
                // Add and save the new computer record
                entityFramework.Computers.Add(myComputer);
                entityFramework.SaveChanges();
                Console.WriteLine("Record Inserted Successfully");
            }
        }
        static void SelectSql(DataContextDapper dapper,
        Computer myComputer,
        string sqlSelect)
        {
            IEnumerable<Computer>? computers = dapper.LoadData<Computer>(sqlSelect);
            if (computers != null){
                foreach (Computer singleComputer in computers)
                {
                    Console.WriteLine(singleComputer);
                }
            }
        }

        static void SelectEF(DataContextEF entityFramework){
            IEnumerable<Computer>? computersEF = entityFramework.Computers?.ToList<Computer>();
            if (computersEF != null){
                foreach (Computer singleComputer in computersEF)
                {
                    Console.WriteLine(singleComputer);
                }
            }
        }
    }
}
