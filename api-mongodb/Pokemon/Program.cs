using api_mongodb.Application;
using api_mongodb.Application.Interfaces;
using api_mongodb.ChargeDatabase;
using api_mongodb.ChargeDatabase.Interfaces;
using api_mongodb.Infrastructure.Data;
using api_mongodb.Infrastructure.Data.Interfaces;
using api_mongodb.Infrastructure.Data.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Text;

namespace api_mongodb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            { 
                 options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            builder.Services.AddScoped<IChargeDatabaseCore, ChargeDatabaseCore>();
            builder.Services.AddScoped<IPokemonContext, PokemonContext>();
            builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
            builder.Services.AddScoped<IGetPokemon, GetPokemon>();

            var app = builder.Build();
            if (builder.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            app.MapGet("/", () => "Hello World!");
            app.MapControllers();
            app.Run();
        }
    }
}
