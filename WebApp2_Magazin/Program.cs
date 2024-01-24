
using Autofac.Extensions.DependencyInjection;
using Autofac;
using WebApp2_Magazin.Abstraction;
using WebApp2_Magazin.Mapper;
using WebApp2_Magazin.Services;
using Microsoft.EntityFrameworkCore;
using WebApp2_Magazin.Query;
using WebApp2_Magazin.Mutation;

namespace WebApp2_Magazin
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMemoryCache();
            builder.Services.AddAutoMapper(typeof(MapperProfile));
            //builder.Services.AddPooledDbContextFactory<AppDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("db")));

            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddTransient<IStorageService, StorageService>();
            builder.Services.AddTransient<ICategoryService, CategoryService>();
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(cb =>
            {
                cb.Register(c => new AppDbContext(builder.Configuration.GetConnectionString("db"))).InstancePerDependency();
            });
            //builder.Services.AddDbContext<AppDbContext>(conf => conf.UseNpgsql(builder.Configuration.GetConnectionString("db")));
            builder.Services.AddGraphQL();

            builder.Services
                .AddGraphQLServer()
                .AddQueryType<MySimpleQuery>()
                .AddMutationType<MySimpleMutation>();


            var app = builder.Build();

            app.MapGraphQL();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            app.Run();
        }
    }
}