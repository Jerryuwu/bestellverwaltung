using System;
using System.Collections.Generic;
using System.Data;
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
                    Percentage = reader.GetDecimal("percentage")
                };
            }
        }

        public Task<TaxEntity> GetTaxById(int id) {
            throw new System.NotImplementedException();
        }

        public async Task<TaxEntity> SaveOrUpdateTax(TaxEntity entity) {
            await using var db = new Database();
            await db.Connection.OpenAsync();
            await using var cmd = new MySqlCommand("insert into taxes (id, percentage) values (@id, @percentage) on duplicate key update id = @id, percentage = @percentage",
                db.Connection);
            cmd.Parameters.AddRange(new MySqlParameter[] {
                new("id", entity.Id),
                new("percentage", entity.Percentage)
            });
            await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
            entity.Id ??= Convert.ToInt32(cmd.LastInsertedId);
            return entity;
        }

        public async Task DeleteTax(TaxEntity entity) {
            await using var db = new Database();
            await db.Connection.OpenAsync();
            await using var cmd = new MySqlCommand("delete from taxes where id = @id", db.Connection);

            cmd.Parameters.AddWithValue("id", entity.Id);
            await cmd.ExecuteNonQueryAsync();
        }
    }
}