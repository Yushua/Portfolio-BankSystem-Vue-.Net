using System;
using System.Collections.Generic;
using Dapper;
using Microsoft.Data.SqlClient;
using MyApp.Data;
using MyApp.Models;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataContextDapper dapper = new DataContextDapper();
            DataContextEF entityFramework = new DataContextEF();

            Computer myComputer = new Computer
            {
                Motherboard = "HIghter3",
                VideoCard = "new VideoCard",
                CpuCores = 5,
                HasWifi = true,
                ReleaseDate = DateTime.Now, // Directly using DateTime
                Price = 933.90m
            };
            // InsertSql(dapper, myComputer);
            // SelectSql(dapper, myComputer);

            InsertEF(entityFramework, myComputer);
            SelectEF(entityFramework);
        }

        static void InsertSql(DataContextDapper dapper, Computer myComputer)
        {

            // Check if the record already exists
            string checkSql = @"SELECT COUNT(*)
                                FROM TutorialAppSchema.Computer
                                WHERE Motherboard = @Motherboard";

            // Retrieve count as int
            int count = dapper.LoadDataSingle<int>(checkSql, new { Motherboard = myComputer.Motherboard });

            if (count > 0)
            {
                Console.WriteLine("The record already exists.");
            }
            else
            {
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
                    @Price
                )";

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

        static void InsertEF(DataContextEF entityFramework, Computer myComputer)
        {
            // Check if a computer with the same motherboard already exists
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
        static void SelectSql(DataContextDapper dapper, Computer myComputer)
        {
            string sqlSelect = @"SELECT
                ComputerId,
                Motherboard,
                VideoCard,
                CPUCores,
                HasWifi,
                ReleaseDate,
                Price
            FROM TutorialAppSchema.Computer";

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
