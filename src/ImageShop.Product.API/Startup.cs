using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ImageShop.Mappers.Abstractions;
using ImageShop.Product.Application.Queries;
using ImageShop.Product.Infrastructure.Cosmos.Repositories;
using ImageShop.Product.Domain.ProductAggregate;
using System.IO;
using System.Reflection;
using ImageShop.Data.Abstraction;
using ImageShop.Product.Infrastructure.Cosmos;
using ImageShop.Data.Cosmos;
using ImageShop.Product.Infrastructure.Cosmos.Models;
using ImageShop.Product.Application.Behaviour;
using FluentValidation;
using ImageShop.Validators.Abstractions;
using ImageShop.Product.API.Filters;
using ImageShop.Product.Domain.ReviewAggregate;

namespace ImageShop.Product.API
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

            services.AddControllers(x => x.Filters.Add(typeof(ExceptionFilter)));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ImageShop.Product.API", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            });

            //AutoMapper
            services.AddAutoMapper(typeof(BaseMapperProfile).Assembly);

            //validation
            services.AddValidatorsFromAssembly(typeof(BaseAbstractValidator<>).Assembly);

            //Mediatr
            services.AddMediatR(typeof(GetProductsQueryHandler).Assembly);

            //Mediatr Behaviour
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            //interfaces
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();

            //Cosmos DB
            AddCosmosDbConfigurations(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ImageShop.Product.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddCosmosDbConfigurations(IServiceCollection services)
        {
            //get from app settings
            CosmosDbConfiguration cosmosDbConfiguration = new CosmosDbConfiguration();
            var cosmosDbConfigurationSection = Configuration.GetSection("CosmosDbConfiguration");
            cosmosDbConfigurationSection.Bind(cosmosDbConfiguration);

            services.Configure<CosmosDbConfiguration>(cosmosDbConfigurationSection);

            services.AddScoped<ICosmosRepository<ProductModel>>(provider => new CosmosRepository<ProductModel>(cosmosDbConfiguration.EndpointUri, cosmosDbConfiguration.PrimaryKey, cosmosDbConfiguration.DatabaseId, "products"));
            services.AddScoped<ICosmosRepository<ReviewModel>>(provider => new CosmosRepository<ReviewModel>(cosmosDbConfiguration.EndpointUri, cosmosDbConfiguration.PrimaryKey, cosmosDbConfiguration.DatabaseId, "reviews"));
        }
    }
}
