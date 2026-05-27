
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MinhaPrimeiraApi.Data;
using MinhaPrimeiraApi.Integracao;
using MinhaPrimeiraApi.Integracao.Interfaces;
using MinhaPrimeiraApi.Integracao.Response;
using MinhaPrimeiraApi.Integracao.Response.Refit;
using MinhaPrimeiraApi.Repositorios;
using MinhaPrimeiraApi.Repositorios.Interfaces;
using Refit;
using System.Text;

namespace MinhaPrimeiraApi
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            string chaveSecrta = "6ef2c204-f262-40aa-be9f-1385bbc15137";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<BancoContext>(options =>
            {
                var connectionString =
                    builder.Configuration.GetConnectionString("Database");

                options.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString));
            });
            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            builder.Services.AddRefitClient<IViaCepIntegracaoRefit>().ConfigureHttpClient(
                c=> c.BaseAddress= new Uri("https://viacep.com.br")
                );
            builder.Services.AddScoped<IViaCepIntegracao, ViaCepIntegracao>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Usuario - Api", Version = "v1" });
                var securitySchema = new OpenApiSecurityScheme
                {
                    Name = "JWt Autenticação ",
                    Description ="Enrtre com o JWt Barrer token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat ="JWT",
                    Reference = new OpenApiReference
                    {
                        Id= JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securitySchema);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securitySchema,new string[] { }}
                });
            });


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "sua_empresa",
                    ValidAudience = "sua_aplicacao",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecrta))
                };
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
