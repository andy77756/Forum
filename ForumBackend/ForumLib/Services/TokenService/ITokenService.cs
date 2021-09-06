﻿using System.Threading.Tasks;

namespace ForumLib.Services.TokenService
{
    public interface ITokenService
    {
        public Task<string> GenerateJwtAsync(int userId);

        public Task<bool> CheckJwtIsValidAsync(string jti);

        public Task DeleteJtiAsync(string jti);

        
    }
}
