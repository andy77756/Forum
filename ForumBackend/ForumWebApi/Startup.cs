using ForumDAL.Repositories;
using ForumLib.Extensions;
using ForumLib.Models;
using ForumLib.Services.ForumService;
using ForumLib.Services.LoginService;
using ForumLib.Services.RegisterService;
using ForumLib.Services.TokenService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace ForumWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers(config =>
            {
                //config.Filters.Add(typeof(AuthorizationFilter));
            }).AddJsonOptions(option =>
            {
                option.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ForumWebApi", Version = "v1" });
            });

            var jwtConfig = Configuration.GetSection("Jwt").Get<JwtConfig>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                        ValidateIssuer = true,
                        ValidIssuer = jwtConfig.Issuer,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SignKey))
                    };
                });

            var corsUrl = Configuration.GetSection("CorsUrl").Value.Split(',');

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.WithOrigins(corsUrl)
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            services.AddHttpContextAccessor();

            services.AddEncryptHelper(options => Configuration.GetSection("Encrypt").Bind(options));
            services.AddJwtHelper(options => Configuration.GetSection("jwt").Bind(options));

            services.AddSingleton<IAuthorizeStoreProcedure, AuthorizeStoreProcedure>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IReplyRepository, ReplyRepository>();
            services.AddSingleton<ITokenService, TokenService>();
            services.AddSingleton<ILoginService, LoginService>();
            services.AddSingleton<IRegisterService, RegisterService>();
            services.AddScoped<IForumService, ForumService>();
            services.AddScoped<IPostStoreProcedure, PostStoreProcedure>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ForumWebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
