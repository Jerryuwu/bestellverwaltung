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

        public Task<ArticleEntity> SaveOrUpdateArticle(ArticleEntity entity) {
            throw new System.NotImplementedException();
        }
    }
}