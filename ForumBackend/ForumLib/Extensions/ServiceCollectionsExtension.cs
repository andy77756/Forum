using ForumLib.Helpers;
using ForumLib.Models;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ForumLib.Extensions
{
    /// <summary>
    /// 針對 IServiceCollection 物件擴充類別
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 增加 EncryptHelper 服務實體
        /// </summary>
        /// <param name="serviceCollection">serviceCollection</param>
        /// <param name="options">options</param>
        /// <returns></returns>
        public static IServiceCollection AddEncryptHelper(this IServiceCollection serviceCollection, Action<EncryptConfig> options)
        {
            serviceCollection.AddScoped<EncryptHelper>();

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), @"Please provide options for EncryptHelper.");
            }

            serviceCollection.Configure(options);
            return serviceCollection;
        }

        /// <summary>
        /// 增加 JwtHelper 服務實體
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddJwtHelper(this IServiceCollection serviceCollection, Action<JwtConfig> options)
        {
            serviceCollection.AddScoped<JwtHelper>();

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), @"Please provide options for JwtHelper.");
            }

            serviceCollection.Configure(options);
            return serviceCollection;
        }
    }
}
