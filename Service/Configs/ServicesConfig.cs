using AutoMapper;
using Service.Mappers;
using Domain.Repository.Implementation;
using Domain.Repository.Interfaces;
using Domain.Validators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Service.Implementation;
using Service.Interfaces;
using Service.Validators;

namespace Service.Configs
{
    public static class ServicesConfig
    {
        public static void AddInternalServices(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();

            services.AddScoped<ProductValidator>(); 
            services.AddScoped<CategoryValidator>();


        }
        public static void AddConfig(this IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
            
        }
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                  new OpenApiInfo
                  {
                      Title = "Catalog API",
                      Version = "v1",
                      Description = "Catalog API",
                      Contact = new OpenApiContact
                      {
                          Name = "Catalog API",
                          Email = "fabiomercadon@gmail.com"
                      }
                  });
            });
        }
    }
}
