using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bestellverwaltung.WPF.Entities;
using Bestellverwaltung.WPF.ViewModels;
using MySqlConnector;

namespace IFAS2Personal.WPF.Database {
    public interface IArticleRepository {
        public IAsyncEnumerable<ArticleEntity> GetAllArticles();
        public Task<ArticleEntity> GetArticleById(int id);
        public Task<ArticleEntity> SaveOrUpdateArticle(ArticleEntity entity);

        public Task DeleteArticle(ArticleEntity entity);
    }
    public class ArticleRepository : IArticleRepository{
        public async IAsyncEnumerable<ArticleEntity> GetAllArticles() {
            await using var db = new Database();
            await db.Connection.OpenAsync().ConfigureAwait(false);
            await using var cmd = new MySqlCommand("select * from article", db.Connection);

            await using var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
            while (await reader.ReadAsync().ConfigureAwait(false)) {
                yield return new ArticleEntity() {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    Price = reader.GetDecimal("price"),
                    Stock = reader.GetInt32("stock")
                };
            }
        }

        public Task<ArticleEntity> GetArticleById(int id) {
            throw new System.NotImplementedException();
        }

        public async Task<ArticleEntity> SaveOrUpdateArticle(ArticleEntity entity) {
            await using var db = new Database();
            await db.Connection.OpenAsync();
            await using var cmd = new MySqlCommand("insert into article (id, name, price, stock) values (@id, @name, @price, @stock) on duplicate key update id = @id, name = @name, price = @price, stock = @stock",
                db.Connection);
            cmd.Parameters.AddRange(new MySqlParameter[] {
                new("id", entity.Id),
                new("name", entity.Name),
                new("price", entity.Price),
                new("stock", entity.Stock)
            });
            await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
            entity.Id ??= Convert.ToInt32(cmd.LastInsertedId);
            return entity;
        }

        public async Task DeleteArticle(ArticleEntity entity) {
            await using var db = new Database();
            await db.Connection.OpenAsync().ConfigureAwait(false);
            await using var cmd = new MySqlCommand("delete from article where id = @id", db.Connection);

            cmd.Parameters.Add(new MySqlParameter("id", entity.Id));
            await cmd.ExecuteNonQueryAsync();
        }
    }
}