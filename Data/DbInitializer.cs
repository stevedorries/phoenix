using System.Globalization;
using System.Data;
using System.IO;
using System.Linq;
using CsvHelper;
using Dapper;
using phoenix.Models;
using System;

namespace phoenix.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context) {
            string basePath = $"Data{Path.DirectorySeparatorChar}";
            using(var conn = context.CreateConnection()) {
                using(var reader = new StreamReader($"{basePath}clients.csv"))
                using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture)){
                    var clients = csv.GetRecords<Client>().ToList();
                    foreach (var client in clients)
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("id",client.Id,DbType.Int32);
                        parameters.Add("fName",client.FirstName,DbType.String);
                        parameters.Add("lName",client.LastName,DbType.String);
                        parameters.Add("email",client.Email,DbType.String);
                        try {conn.Execute("INSERT INTO clients (id, first_name, last_name, email) VALUES (@id,@fName,@lName,@email);",parameters);}
                        catch(Exception e) {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                using(var reader = new StreamReader($"{basePath}credit.csv"))
                using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture)){
                    var clients = csv.GetRecords<Credit>().ToList();
                    foreach (var client in clients)
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("id",client.Id,DbType.Int32);
                        parameters.Add("client",client.ClientId,DbType.Int32);
                        parameters.Add("score",client.CreditScore,DbType.Int32);
                        try {conn.Execute("INSERT INTO credit (id, client_id, credit_score) VALUES (@id,@client,@score);",parameters);}
                        catch(Exception e) {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                using(var reader = new StreamReader($"{basePath}applications.csv"))
                using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture)){
                    var clients = csv.GetRecords<Application>().ToList();
                    foreach (var client in clients)
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("id",client.Id,DbType.Int32);
                        parameters.Add("client",client.ClientId,DbType.Int32);
                        parameters.Add("income",client.AnnualIncome,DbType.Single);
                        parameters.Add("debt",client.MonthlyDebt,DbType.Single);
                        try {conn.Execute("INSERT INTO applications (id, client_id, annual_income,monthly_debt) VALUES (@id,@client,@income,@debt);",parameters);}
                        catch(Exception e) {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
            }
        }
    }
}