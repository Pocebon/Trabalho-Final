using System;
using System.Collections.Generic;


using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Trab_Final.BaseDados.Models;
using Trab_Final.Mapping;

namespace Trab_Final
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddAutoMapper(typeof(DomainToDTOMapping));
            builder.Services.AddControllers().AddXmlDataContractSerializerFormatters();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<ApiJulianoContext>();
            builder.Services.AddScoped<AutorService>();
            builder.Services.AddScoped<PessoaService>();
            builder.Services.AddScoped<EditoraService>();
            builder.Services.AddScoped<EmprestimoService>();
            builder.Services.AddScoped<LivroService>();
            builder.Services.AddScoped<EmprestimoLivroService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
