using System.Data;
using System.Data.SQLite;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace phoenix.Data
{
    public class ApplicationDbContext
    {
        private readonly IConfiguration _config;
        private readonly string _connString;

        public ApplicationDbContext(IConfiguration config)
        {
            _config = config;
            _connString = _config.GetConnectionString("SQLite");
        }

        public void EnsureCreated()
        {
            if (_connString != ":memory")
            {
                SQLiteConnection.CreateFile(_connString);
            }
            using (var conn = CreateConnection())
            {
                conn.Execute(@"CREATE TABLE IF NOT EXISTS clients (
id INTEGER PRIMARY KEY,
first_name TEXT,
last_name TEXT,
email TEXT);");
                conn.Execute(@"CREATE TABLE IF NOT EXISTS applications (
id INTEGER PRIMARY KEY,
client_id INTEGER,
annual_income REAL,
monthly_debt REAL,
FOREIGN KEY (client_id) REFERENCES clients(id)
                  );");

                conn.Execute(@"CREATE TABLE IF NOT EXISTS credit (
id INTEGER PRIMARY KEY,
client_id INTEGER,
credit_score INTEGER,
FOREIGN KEY (client_id) REFERENCES clients(id)
);");
            }
        }

        public IDbConnection CreateConnection()
        {
            return new SQLiteConnection($"Data Source={_connString};Version=3;Foreign Keys=True;");
        }
    }
}