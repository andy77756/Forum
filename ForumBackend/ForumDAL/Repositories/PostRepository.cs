using Dapper;
using ForumDAL.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ForumDAL.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly IConfiguration Configuration;

        public PostRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<int> AddAsync(Post entity)
        {
            var sql = "INSERT INTO t_posts (f_userId, f_topic, f_content, f_createAt, f_updateAt) VALUES (@f_userId, @f_topic, @f_content, @f_createAt, @f_updateAt)";

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }

        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM t_posts WHERE f_id = @f_id";

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { f_id = id });

                return result;
            }
        }

        public async Task<IReadOnlyList<Post>> GetAllAsync()
        {
            var sql = "SELECT f_id, f_userId, f_topic, f_content, f_createAt, f_updateAt FROM t_posts WITH(NOLOCK)";

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Post>(sql);

                return result.ToList();
            }
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            var sql = "SELECT f_id, f_userId, f_topic, f_content, f_createAt, f_updateAt FROM t_posts WITH(NOLOCK) WHERE f_id = @f_id ";

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Post>(sql, new { f_id = id});

                return result;
            }
        }

        public async Task<int> UpdateAsync(Post entity)
        {
            var sql = "UPDATE User WITH(ROWLOCK) SET f_topic = @f_topic, f_content = @f_content, f_updateAt = @f_updateAt";

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);

                return result;
            }
        }
    }
}
