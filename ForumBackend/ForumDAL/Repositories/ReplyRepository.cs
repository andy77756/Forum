using Dapper;
using ForumDAL.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ForumDAL.Repositories
{
    public class ReplyRepository : IReplyRepository
    {
        private readonly IConfiguration Configuration;

        public ReplyRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<int> AddAsync(Reply entity)
        {
            var sql = "INSERT INTO t_replies (f_userId, f_postId, f_content, f_createAt) VALUES (@f_email, @f_postId, @f_content, @f_createAt)";

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }

        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM t_replies WHERE f_id = @f_id";

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { f_id = id });

                return result;
            }
        }

        public async Task<IReadOnlyList<Reply>> GetAllAsync()
        {
            var sql = "SELECT f_id, f_userId, f_postId, f_content, f_createAt FROM t_replies WITH(NOLOCK)";

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Reply>(sql);

                return result.ToList();
            }
        }

        public async Task<Reply> GetByIdAsync(int id)
        {
            var sql = "SELECT f_id, f_userId, f_postId, f_content, f_createAt FROM t_replies WITH(NOLOCK) WHERE f_id = @f_id ";

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Reply>(sql, new { f_id = id});

                return result;
            }
        }

        public async Task<int> UpdateAsync(Reply entity)
        {
            var sql = "UPDATE User WITH(ROWLOCK) SET f_content = @f_content";

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);

                return result;
            }
        }
    }
}
