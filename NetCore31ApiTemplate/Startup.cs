using System.Drawing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCore31ApiTemplate.NSwag;

namespace NetCore31ApiTemplate
{
    public class Startup
    {
        private readonly string _corsKey;
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _corsKey = "CorsPolicy";
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy(_corsKey,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services.AddOpenApiDocument(configure =>
            {
                configure.Title = "API";
                configure.DocumentName = "API";
                configure.Description = "An API interface.";
                configure.OperationProcessors.Add(new HeaderParameterOperationProcessor());
                configure.DocumentProcessors.Add(new SchemaExtenderDocumentProcessor());

                //TODO: uncomment this if you need to put JWTs in Authorization header (Http Helpers has a function to extract it from request)
                //configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                //{
                //    Type = OpenApiSecuritySchemeType.ApiKey,
                //    Name = "Authorization",
                //    In = OpenApiSecurityApiKeyLocation.Header,
                //    Description = "Type into the textbox: {Firebase JWT Token}"
                //});

                //configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });

            ServiceRegistry.AddConfigs(services, _configuration);

            ServiceRegistry.ScanForAllRemainingRegistrations(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.IsStaging())
            {
                //home server specific config here
            }

            app.UseOpenApi();

            app.UseSwaggerUi3();

            app.UseReDoc(options =>
            {
                options.Path = "/api-docs";
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(_corsKey);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
