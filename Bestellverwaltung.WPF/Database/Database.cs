using System;
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
    }
}