using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace CleanArchMvc.Infra.IoC
{
    public static class DependencyInjectionSwagger
    {
        public static IServiceCollection AddInfrastructureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CleanArchMVC.API",
                    Description = "Creating .NET Core Projects According to the Clean Architecture",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Roniel da Silva Alves",
                        Email = "roniel.alves@gmail.com",
                        Url = new Uri("https://github.com/alvesRoniel")
                    }
                });

                //DEFINE AS CONFIGURAÇOES DE SEGURANÇA PARA PODER USAR O JWT NO SWAGGER
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    //definir configuracoes
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] " +
                   "and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
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

                //PARA SALVAR O XML COM A DOCUMENTAÇ;AO DOS ENDPOINTS
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
               // c.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
