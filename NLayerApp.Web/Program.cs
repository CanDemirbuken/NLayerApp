using Autofac.Extensions.DependencyInjection;
using Autofac;
using NLayerApp.Web.Modules;
using Microsoft.EntityFrameworkCore;
using NLayerApp.Repository;
using System.Reflection;
using NLayerApp.Service.Mapping;
using FluentValidation.AspNetCore;
using NLayerApp.Service.Validation;
using NLayerApp.Web.Services;

namespace NLayerApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());
            builder.Services.AddAutoMapper(typeof(MapProfile));

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), opt =>
                {
                    opt.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
                });
            });

            builder.Services.AddHttpClient<ProductApiService>(options =>
            {
                options.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
            });

            builder.Services.AddHttpClient<CategoryApiService>(options =>
            {
                options.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
            });

            builder.Services.AddScoped(typeof(NotFoundFilter<>));
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));

            var app = builder.Build();

            app.UseExceptionHandler("/Home/Error");
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}