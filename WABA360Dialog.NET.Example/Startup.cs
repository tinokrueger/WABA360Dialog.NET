using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Serilog;
using WABA360Dialog.ApiClient.Interfaces;
using WABA360Dialog.PartnerClient.Interfaces;
using WABA360Dialog.PartnerClient.Models;

namespace WABA360Dialog.NET.Example
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
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddSingleton<IWABA360DialogApiClient>(new WABA360DialogApiClient(Configuration["WABA360Dialog:ChannelApiKey"], new HttpClient()));
            services.AddSingleton<IWABA360DialogPartnerClient>(new WABA360DialogPartnerClient(new PartnerInfo(Configuration["WABA360Dialog:PartnerId"]), Configuration["WABA360Dialog:PartnerToken"], new HttpClient()));

            services
                .AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });
            
            services
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "WABA360Dialog.NET.Example", Version = "v1" });
                    options.AddSecurityDefinition("360Dialog Channel ApiKey", new OpenApiSecurityScheme()
                    {
                        In = ParameterLocation.Header,
                        Name = "360DialogChannelApiKey",
                        Description = "WABA 360Dialog Channel Api Key",
                    });
                    options.AddSecurityDefinition("Partner Id", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Name = "PartnerId",
                        Description = "Partner Id",
                    });

                    options.AddSecurityDefinition("Partner Token", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Partner Token",
                        Name = "PartnerToken",
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "360Dialog Channel ApiKey"
                                }
                            },

                            new string[] { }
                        },
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Partner Id"
                                }
                            },

                            new string[] { }
                        },
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Partner Token"
                                }
                            },

                            new string[] { }
                        },
                    });
                })
                .AddSwaggerGenNewtonsoftSupport();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WABA360Dialog.NET.Example v1"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}