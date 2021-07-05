using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bestellverwaltung.WPF.Entities;
using MySqlConnector;

namespace IFAS2Personal.WPF.Database {
    public interface IDeliveryRepository {
        public IAsyncEnumerable<DeliveryCostEntity> GetAllDeliveryCosts();
        public Task<DeliveryCostEntity> GetDeliveryCostById(int id);
        public Task<DeliveryCostEntity> SaveOrUpdateDeliveryCost(DeliveryCostEntity entity);
        public Task DeleteDeliveryCost(DeliveryCostEntity entity);
    }
    public class DeliveryRepository : IDeliveryRepository {
        public async IAsyncEnumerable<DeliveryCostEntity> GetAllDeliveryCosts() {
            await using var db = new Database();
            await db.Connection.OpenAsync().ConfigureAwait(false);
            await using var cmd = new MySqlCommand("select * from deliverycosts", db.Connection);

            await using var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
            while (await reader.ReadAsync().ConfigureAwait(false)) {
                yield return new DeliveryCostEntity {
                    Amount = reader.GetDecimal("amount"),
                    Bonus = reader.GetDecimal("bonus")
                };
            }
        }

        public Task<DeliveryCostEntity> GetDeliveryCostById(int id) {
            throw new System.NotImplementedException();
        }

        public async Task<DeliveryCostEntity> SaveOrUpdateDeliveryCost(DeliveryCostEntity entity) {
            await using var db = new Database();
            await db.Connection.OpenAsync();
            await using var cmd = new MySqlCommand("insert into deliverycosts (amount, bonus) values (@amount, @bonus) on duplicate key update amount = @amount, bonus = @bonus",
                db.Connection);
            cmd.Parameters.AddRange(new MySqlParameter[] {
                new("amount", entity.Amount),
                new("bonus", entity.Bonus)
            });
            await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
            entity.Amount ??= Convert.ToDecimal(cmd.LastInsertedId);
            return entity;
        }

        public async Task DeleteDeliveryCost(DeliveryCostEntity entity) {
            await using var db = new Database();
            await db.Connection.OpenAsync();
            await using var cmd = new MySqlCommand("delete from deliverycosts where amount = @amount", db.Connection);

            cmd.Parameters.AddWithValue("amount", entity.Amount);
            await cmd.ExecuteNonQueryAsync();        }
    }
}