using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using SenaiRH_G2.Contexts;
using SenaiRH_G2.Interfaces;
using SenaiRH_G2.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SenaiRH_G2
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
            services
                .AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });
            services.AddDbContext<senaiRhContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("Default")));


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "SENAIRH.webApi"
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                                builder =>
                                {
                                    builder.WithOrigins("http://localhost:3000")
                                    .AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                                });
            });

            services
                    .AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = "JwtBearer";
                        options.DefaultChallengeScheme = "JwtBearer";
                    })

                    .AddJwtBearer("JwtBearer", options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("SenaiRH_G2-chave-autenticacao")),
                            ClockSkew = TimeSpan.FromMinutes(30),
                            ValidIssuer = "SENAIRH.webAPI",
                            ValidAudience = "SENAIRH.webAPI"
                        };

                    });

            services.AddTransient<DbContext, senaiRhContext>();
            services.AddTransient<IComentarioCursoRepository, ComentarioCursoRepository>();
            services.AddTransient<IComentarioDescontoRepository, ComentarioDescontoRepository>();
            services.AddTransient<ICursoRepository, CursoRepository>();
            services.AddTransient<IEmpresaRepository, EmpresaRepository>();
            services.AddTransient<IDescontoRepository, DescontoRepository>();
            services.AddTransient<IFavoritosCursoRepository, FavoritosCursoRepository>();
            services.AddTransient<IFavoritosDescontoRepository, FavoritosDescontoRepository>();
            services.AddTransient<ICepRepository, CepRepository>();
            services.AddTransient<ILogradouroRepository, LogradouroRepository>();
            services.AddTransient<IBairroRepository, BairroRepository>();
            services.AddTransient<ICidadeRepository, CidadeRepository>();
            services.AddTransient<IEstadoRepository, EstadoRepository>();
            services.AddTransient<ILocalizacaoRepository, LocalizacaoRepository>();
            services.AddTransient<IRegistrodescontoRepository, RegistrodescontoRepository>();
            services.AddTransient<IRegistrocursoRepository, RegistrocursoRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SenaiRH_G2.webAPI");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles/Images")),
                RequestPath = "/img"
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
