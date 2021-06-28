using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bestellverwaltung.WPF.Entities;
using MySqlConnector;

namespace IFAS2Personal.WPF.Database {
    public interface ICompanyRepository {
        public IAsyncEnumerable<CompanyEntity> GetAllCompanies();
        public Task<CompanyEntity> GetCompanyById(int id);
        public Task<CompanyEntity> SaveOrUpdateCompany(CompanyEntity entity);

        public Task DeleteCompany(CompanyEntity entity);
    }
    public class CompanyRepository : ICompanyRepository{
        public async IAsyncEnumerable<CompanyEntity> GetAllCompanies() {
            await using var db = new Database();
            await db.Connection.OpenAsync().ConfigureAwait(false);
            await using var cmd = new MySqlCommand("select * from companies", db.Connection);

            await using var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
            while (await reader.ReadAsync().ConfigureAwait(false)) {
                yield return new CompanyEntity() {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    Address = reader.GetString("address"),
                    Postcode = reader.GetString("postcode"),
                    City = reader.GetString("city"),
                    Telephone = reader.GetString("telephone"),
                    Fax = reader.GetString("fax"),
                    Owner = reader.GetString("owner")
                };
            }        }

        public Task<CompanyEntity> GetCompanyById(int id) {
            throw new System.NotImplementedException();
        }

        public async Task<CompanyEntity> SaveOrUpdateCompany(CompanyEntity entity) {
            await using var db = new Database();
            await db.Connection.OpenAsync();
            await using var cmd = new MySqlCommand("insert into companies (id, name, address, postcode, city, telephone, fax, owner) values (@id, @name, @address, @postcode, @city, @telephone, @fax, @owner) on duplicate key update id = @id, name = @name, address = @address, postcode = @postcode, city = @city, telephone = @telephone, fax = @fax, owner = @owner",
                db.Connection);
            cmd.Parameters.AddRange(new MySqlParameter[] {
                new("id", entity.Id),
                new("name", entity.Name),
                new("address", entity.Address),
                new("postcode", entity.Postcode),
                new("city", entity.City),
                new("telephone", entity.Telephone),
                new("fax", entity.Fax),
                new("owner", entity.Owner),
            });
            await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
            entity.Id ??= Convert.ToInt32(cmd.LastInsertedId);
            return entity;
        }

        public async Task DeleteCompany(CompanyEntity entity) {
            await using var db = new Database();
            await db.Connection.OpenAsync().ConfigureAwait(false);
            await using var cmd = new MySqlCommand("delete from companies where id = @id", db.Connection);

            cmd.Parameters.Add(new MySqlParameter("id", entity.Id));
            await cmd.ExecuteNonQueryAsync();
        }
    }
}