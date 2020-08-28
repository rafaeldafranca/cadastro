using Cadastro.Core.Contexts;
using Cadastro.Core.Repo;
using Cadastro.Domain.Interfaces.Repos;
using Cadastro.Domain.Interfaces.Services;
using Cadastro.Services;
using Cadastro.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadastro.IoC
{
    public static class IocExtension
    {
        /*------------------------------------------------------------------------------------------------

          APENAS PARA TESTAR AS CONSULTAS DO DAPPER. DAPPER NÃO UTILIZA BANCOS EM MEMORIA.
          NECESSARIO SERVIDOR, CRIAR O BANCO E APLICAR MIGRATIONS. Configs no appSettings.Json

          Rodar comandos no Console do package manager e usar o options do contexto "UseSqlServer" comentado 
          no lugar do UseInMemory e trocar a injeção para UserDapperRepo:

              GERAR SCRIPT    : add-migration script -Project Cadastro.Core -Context PrincipalContext
              GERAR BANCO     : update-database -Project Cadastro.Core -Context PrincipalContext 

          ------------------------------------------------------------------------------------------------*/

        public static void Configure(this IServiceCollection services, IConfiguration configuration)
        {
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
