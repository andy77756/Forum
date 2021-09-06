using ForumLib.Models;
using Microsoft.Extensions.Options;
using System;
using System.Security.Cryptography;
using System.Text;

namespace ForumLib.Helpers
{
    public class EncryptHelper
    {
        private readonly EncryptConfig _encryptConfig;

        public EncryptHelper(IOptions<EncryptConfig> encryptOptions)
        {
            _encryptConfig = encryptOptions.Value;
        }

        /// <summary>
        /// 密碼加密 SHA256
        /// </summary>
        /// <param name="password">密碼</param>
        /// <returns></returns>
        public string Encrypt(string password)
        {
            byte[] pwdAndSalt = Encoding.UTF8.GetBytes(password + _encryptConfig.Salt);
            using var sha256Managed = new SHA256Managed();
            byte[] hash = sha256Managed.ComputeHash(pwdAndSalt);
            return Convert.ToBase64String(hash);
        }
    }
}
