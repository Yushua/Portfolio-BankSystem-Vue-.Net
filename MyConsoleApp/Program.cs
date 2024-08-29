using System;
using System.Collections.Generic;
using System.Text.Json;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyApp.Data;
using MyApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
            // Computer myComputer = new Computer
            // {
            //     Motherboard = "HIghter4",
            //     VideoCard = "new VideoCard",
            //     CpuCores = 5,
            //     HasWifi = true,
            //     ReleaseDate = DateTime.Now, // Directly using DateTime
            //     Price = 933.90m
            // };

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

            // ft_MyComputerDapper(myComputer, dapper, checkSql, false, insertSql, true);
            // ft_MyComputerEntityFramework(myComputer, entityFramework);

            // ft_WriteFile(myComputer, insertSql, "all", "log.txt", true);
            // ft_WriteFile(myComputer, insertSql, "stream", "log.txt", true);
            // ft_WriteFile(myComputer, insertSql, "stream", "log.txt", true);

            string jsonFile = ft_ReadFile("Computers.json");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            IEnumerable<Computer>? computersNewtonSoft = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(jsonFile, options);

            IEnumerable<Computer>? computersDeserialise = JsonConvert.DeserializeObject<IEnumerable<Computer>>(jsonFile);

            if (computersNewtonSoft != null)
            {
                foreach (var computer in computersNewtonSoft)
                {
                    Console.WriteLine($"ComputerId: {computer.ComputerId}, Motherboard: {computer.Motherboard}");
                }
            }
            else
            {
                Console.WriteLine("Deserialization failed or returned null.");
            }

            if (computersNewtonSoft != null) 
            {
                foreach (Computer computer in computersNewtonSoft)
                {
                    // Log the motherboard value to check what’s being queried
                    // Console.WriteLine($"Checking motherboard: {computer}");
                    Console.WriteLine($"Checking motherboard: {computer.Motherboard}");

                    string checkSql = @"SELECT COUNT(*)
                                        FROM TutorialAppSchema.Computer
                                        WHERE Motherboard = @Motherboard";

                    // Execute the check SQL to see if the computer exists
                    int count = dapper.LoadDataSingle<int>(checkSql, new { Motherboard = computer.Motherboard });

                    if (count > 0)
                    {
                        Console.WriteLine($"Record with motherboard '{computer.Motherboard}' already exists.");
                    }
                    else
                    {
                        Console.WriteLine($"Inserting new record with motherboard: {computer.Motherboard}");

                        string computerInsertSql = @"INSERT INTO TutorialAppSchema.Computer (
                                                        Motherboard,
                                                        VideoCard,
                                                        CPUCores,
                                                        HasWifi,
                                                        ReleaseDate,
                                                        Price
                                                    ) VALUES (
                                                        @Motherboard,
                                                        @VideoCard,
                                                        @CPUCores,
                                                        @HasWifi,
                                                        @ReleaseDate,
                                                        @Price)";

                        dapper.ExecuteSql(computerInsertSql, new
                        {
                            Motherboard = computer.Motherboard,
                            VideoCard = computer.VideoCard,
                            CPUCores = computer.CpuCores,
                            HasWifi = computer.HasWifi ? 1 : 0,
                            ReleaseDate = computer.ReleaseDate,
                            Price = computer.Price
                        });

                        Console.WriteLine("Record Inserted Successfully");
                    }
                }
            }

            // JsonSerializerSettings settings = new JsonSerializerSettings(){
            //     ContractResolver = new CamelCasePropertyNamesContractResolver()
            // };

            // string computersCon = JsonConvert.SerializeObject(computersNewtonSoft, settings);

            // ft_WriteFile(computersCon, "all", "computerCopySer.txt", false);

            // JsonSerializerOptions option = new JsonSerializerOptions(){
            //     PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            // };
            
            // string computersVer = System.Text.Json.JsonSerializer.Serialize(computersDeserialise, option);

            // ft_WriteFile(computersVer, "all", "computerCoptCon.txt", false);
        }

        static void ft_MyComputerDapper(Computer myComputer, DataContextDapper dapper, string checkSql, bool check, string insertSql, bool insert){
            if (insert){
                InsertSql(dapper, myComputer, checkSql, insertSql);
            } if (check){
                SelectSql(dapper, myComputer, checkSql);
            }
        }

        static void ft_MyComputerEntityFramework(Computer myComputer, DataContextEF entityFramework){
            InsertEF(entityFramework, myComputer);
            SelectEF(entityFramework);
        }

        static void ft_WriteFile(string insertSql, string type, string fileName, bool newLine){   
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

        static void InsertSql(DataContextDapper dapper, Computer myComputer, string checkSql, string insertSql)
        {
            // Use Dapper to execute a scalar SQL statement and get the count of records with the same motherboard.
            int count = dapper.LoadDataSingle<int>(checkSql, new { Motherboard = myComputer.Motherboard });

            if (count > 0)
            {
                Console.WriteLine("The record already exists.");
            }
            else
            {
                dapper.ExecuteSql(insertSql, new
                {
                    Motherboard = myComputer.Motherboard,
                    VideoCard = myComputer.VideoCard,
                    CPUCores = myComputer.CpuCores,
                    HasWifi = myComputer.HasWifi ? 1 : 0,
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
