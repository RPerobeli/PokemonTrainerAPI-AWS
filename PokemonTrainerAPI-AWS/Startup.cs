using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PokemonTrainerAPI.Map;
using PokemonTrainerAPI.Map.Interfaces;
using PokemonTrainerAPI.Repository;
using PokemonTrainerAPI.Repository.Interfaces;
using PokemonTrainerAPI.Services;
using PokemonTrainerAPI.Services.Interfaces;
using PokemonTrainerAPIAWS.Extensions;

namespace PokemonTrainerAPI_AWS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<PkTrainerContext>(options =>
            {
                //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                    new MySqlServerVersion(new System.Version("8.0.27")),
                    providerOptions => providerOptions.EnableRetryOnFailure()
                    );
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PokemonTrainerAPI", Version = "v1" });
                c.ResolveConflictingActions(x => x.First());
            });
            services.AddScoped<IMapper, Mapper>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPokemonRepository, PokemonRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPokemonService, PokemonService>();
            //services.AddScoped<IPokemonClient, PokemonClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
                });
            });
            string urlProducao = Configuration.GetUrlProducao();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
#if DEBUG
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PokemonTrainerAPI v1");
#else
                c.SwaggerEndpoint($"{urlProducao}/swagger/v1/swagger.json", "PokemonTrainerAPI v1");
#endif
                //c.RoutePrefix = string.Empty;
            });
            app.UseHttpsRedirection();
        }
    }
}
