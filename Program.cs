
using Autofac.Extensions.DependencyInjection;
using Autofac;
using WebApp1_Product.Abstraction;
using WebApp1_Product.Models;
using WebApp1_Product.Repositories;
using Microsoft.Extensions.FileProviders;

namespace WebApp1_Product
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingProfiles));
            builder.Services.AddMemoryCache(x =>
            {
                x.TrackStatistics = true;
            });

            // Autofac
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Host.ConfigureContainer<ContainerBuilder>(cb => cb.RegisterType<ProductRepository>()
                        .As<IProductRepository>());

            builder.Host.ConfigureContainer<ContainerBuilder>(cb => cb.RegisterType<GroupRepository>()
            .As<IGroupRepository>());

            //builder.Services.AddSingleton<IProductRepository, ProductRepository>();
            //builder.Services.AddSingleton<IGroupRepository, GroupRepository>();

            var confBuilder = new ConfigurationBuilder();
            confBuilder.SetBasePath(Directory.GetCurrentDirectory());
            confBuilder.AddJsonFile("appsettings.json");
                    

            var autoFacconf = confBuilder.Build();

            var app = builder.Build();



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            var staticFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles");
            if (!Directory.Exists(staticFilesPath))
            {
                Directory.CreateDirectory(staticFilesPath);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(staticFilesPath),
                RequestPath = "/static"
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}