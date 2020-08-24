using Cadastro.Core.Contexts;
using Cadastro.Core.Repo;
using Cadastro.Domain.Interfaces.Repos;
using Cadastro.Domain.Interfaces.Services;
using Cadastro.Services;
using Cadastro.Services.Configs;
using Cadastro.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;

namespace Cadastro
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            ConfigureSwagger(services);
            ConfigureAuthentication(services);
            ConfigureID(services);
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Cadastro de Usuarios");
            });

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Cadastro",
                        Version = "v1",
                        Description = "Api de cadastro de usuarios",
                        Contact = new OpenApiContact
                        {
                            Name = "Rafael França",
                            Email = "rafaelfranca@live.com",
                        }
                    }); ;

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. Please insert JWT with Bearer into field"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
            });
        }
        private void ConfigureAuthentication(IServiceCollection services)
        {
            services.AddMvcCore().AddJsonOptions(opcoes =>
            {
                opcoes.JsonSerializerOptions.IgnoreNullValues = true;

            });

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            var signingConfigurations = new SigningConfig();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfig();
            new ConfigureFromConfigurationOptions<TokenConfig>(
                Configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;
                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }
        private void ConfigureID(IServiceCollection services)
        {
            /*------------------------------------------------------------------------------------------------
             
            APENAS PARA TESTAR AS CONSULTAS DO DAPPER. DAPPER NÃO UTILIZA BANCOS EM MEMORIA.
            NECESSARIO SERVIDOR, CRIAR O BANCO E APLICAR MIGRATIONS. Configs no appSettings.Json
              
            Rodar comandos no Console do package manager e usar o options do contexto "UseSqlServer" comentado 
            no lugar do UseInMemory e trocar a injeção para UserDapperRepo:

                GERAR SCRIPT    : add-migration script -Project Cadastro.Core -Context PrincipalContext
                GERAR BANCO     : update-database -Project Cadastro.Core -Context PrincipalContext 
            
            ------------------------------------------------------------------------------------------------*/

            services.AddDbContext<PrincipalContext>(opt =>
            opt.UseInMemoryDatabase("Cadastro"));
            //opt.UseSqlServer(Configuration.GetConnectionString("CnSqlServer")));
            //services.AddTransient<IUserRepo, UserDapperRepo>();
            services.AddTransient<IUserRepo, UserRepo>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IUserService, UserService>();
        }

    }
}
