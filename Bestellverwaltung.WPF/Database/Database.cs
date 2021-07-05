using System;
using System.Data;
using System.Threading.Tasks;
using MySqlConnector;

namespace IFAS2Personal.WPF.Database {
    public class Database : IAsyncDisposable{
        public MySqlConnection Connection { get; set; }
        public Database() {
            Connection = new MySqlConnection(Settings.MySqlConnectionString);
        } 
        public async ValueTask DisposeAsync() {
            await Connection.CloseAsync();
        }
        public struct Settings {
            private const string HOST = "localhost";
            private const int PORT = 3306;
            private const string USER = "root";
            private const string PASSWORD = "";
            public const string DATABASE = "bestellverwaltung_schema";

            public static string MySqlConnectionString =>
                new MySqlConnectionStringBuilder {
                    Server = HOST,
                    Port = PORT,
                    UserID = USER,
                    Password = PASSWORD,
                    Database = DATABASE
                }.ConnectionString;
        }
        public async Task<int> GetNextId(string table) {
            if (Connection.State != ConnectionState.Open) return -2;
            await using var cmd =
                new MySqlCommand(
                    "select AUTO_INCREMENT from information_schema.TABLES where TABLE_SCHEMA = ? and TABLE_NAME = ?", Connection);
            cmd.Parameters.Add(new MySqlParameter("schema", Settings.DATABASE));
            cmd.Parameters.Add(new MySqlParameter("table", table));
            var id = await cmd.ExecuteScalarAsync();
            if (!int.TryParse(id?.ToString(), out var idConverted)) return -1;
            return idConverted;
        }
    }
}