using Dapper;
using ForumLib.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ForumLib.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration Configuration;

        public UserRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<int> AddAsync(User entity)
        {
            var sql = "INSERT INTO t_users (f_email, f_userName, f_password, f_createAt) VALUES (@f_email, @f_userName, @f_password, @f_createAt)";

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }

        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM t_users WHERE f_id = @f_id";

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { f_id = id});

                return result;
            }
        }

        public async Task<IReadOnlyList<User>> GetAllAsync()
        {
            var sql = "SELECT f_id, f_email, f_userName, f_password, f_createAt FROM t_users WITH(NOLOCK)"; 

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<User>(sql);

                return result.ToList();
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var sql = "SELECT f_id, f_email, f_userName, f_password, f_createAt FROM t_users WHERE f_id = @f_id WITH(NOLOCK)";

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<User>(sql);

                return result;
            }
        }

        public async Task<bool> IsExist(string account)
        {
            var sql = "SELECT Count(f_id) FROM t_users WITH(NOLOCK)";

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryFirstAsync<int>(sql);

                return (result > 0) ? false : true;
            }
        }

        public async Task<int> UpdateAsync(User entity)
        {
            var sql = "UPDATE User SET f_email = @f_email, f_password = @f_password";

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);

                return result;
            }
        }
    }
}
