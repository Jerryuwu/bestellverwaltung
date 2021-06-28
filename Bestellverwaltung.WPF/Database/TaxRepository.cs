using System.Collections.Generic;
using System.Threading.Tasks;
using Bestellverwaltung.WPF.Entities;
using MySqlConnector;

namespace IFAS2Personal.WPF.Database {
    public interface ITaxRepository {
        public IAsyncEnumerable<TaxEntity> GetAllTaxes();
        public Task<TaxEntity> GetTaxById(int id);
        public Task<TaxEntity> SaveOrUpdateTax(TaxEntity entity);

        public Task DeleteTax(TaxEntity entity);
    }
    public class TaxRepository : ITaxRepository{
        public async IAsyncEnumerable<TaxEntity> GetAllTaxes() {
            await using var db = new Database();
            await db.Connection.OpenAsync().ConfigureAwait(false);
            await using var cmd = new MySqlCommand("select * from taxes", db.Connection);

            await using var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
            while (await reader.ReadAsync().ConfigureAwait(false)) {
                yield return new TaxEntity() {
                    Id = reader.GetInt32("id"),
                    Percentage = reader.GetInt32("percentage")
                };
            }
        }

        public Task<TaxEntity> GetTaxById(int id) {
            throw new System.NotImplementedException();
        }

        public Task<TaxEntity> SaveOrUpdateTax(TaxEntity entity) {
            throw new System.NotImplementedException();
        }

        public Task DeleteTax(TaxEntity entity) {
            throw new System.NotImplementedException();
        }
    }
}