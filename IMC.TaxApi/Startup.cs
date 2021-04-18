using ICM.TaxApi.Models.Entities;
using IMC.TaxApi.Core.Mappers;
using IMC.TaxApi.Core.Providers;
using IMC.TaxApi.Core.Repository;
using IMC.TaxApi.Core.RestClients;
using IMC.TaxApi.Core.Validators;
using IMC.TaxApi.Host;
using IMC.TaxApi.Host.ApiFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMC.TaxApi
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
            services.AddControllers();
            // Swagger documenation
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.OperationFilter<CustomFilters>();
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "TaxApi Swagger Documentation", Version = "v1" });
            });

            // Http Client
            services.AddHttpClient();

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ExceptionHandleFilter));
                options.AllowEmptyInputInBodyModelBinding = true;
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });

            // Api level dependecies 
            services.AddSingleton<ITaxRepository, TaxRepository>();
            services.AddSingleton<ITaxProvider, TaxProvider>();
            services.AddSingleton<ITaxProviderValidator, TaxProviderValidator>();
            services.AddSingleton<ITaxProviderMapper, TaxProviderMapper>();
            services.AddSingleton<ITaxJarApiClient, TaxJarApiClient>();

            services.AddHttpContextAccessor();
            services.AddOptions();
            ConsumerKeyPartnerConfiguration opts = new ConsumerKeyPartnerConfiguration();
            Configuration.GetSection("ConsumerKeyPartnerConfiguration").Bind(opts);
            services.Configure<ConsumerKeyPartnerConfiguration>(Configuration.GetSection("ConsumerKeyPartnerConfiguration"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Documentation");
            });

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
