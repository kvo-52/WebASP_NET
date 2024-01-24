
using Autofac.Extensions.DependencyInjection;
using Autofac;
using WebApp2_Magazin.Abstraction;
using WebApp2_Magazin.Mapper;
using WebApp2_Magazin.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Autofac;
using WebApp2_Magazin.Query;

namespace WebApp2_Magazin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddMemoryCache();
            builder.Services.AddAutoMapper(typeof(MapperProfile));

            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddTransient<IStorageService, StorageService>();
            builder.Services.AddTransient<ICategoryService, CategoryService>();
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(cb =>
            {
                cb.Register(c => new AppDbContext(builder.Configuration.GetConnectionString("db"))).InstancePerDependency();
            });

            //builder.Services.AddGraphQL();

            //builder.Services
            //.AddGraphQLServer()
            //.AddQueryType<MySimpleQuery>()
            //.AddMutationType<MySimpleMutation>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            var app = builder.Build();

            //app.MapGraphQL();
            //AppContext.SetSwitch("MySql.EnableLegacyTimestampBehavior", true);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}