using Microsoft.EntityFrameworkCore;
using shop.Data.Context;
using shop.Data.Dal;
using shop.Middleware;
using shop.Services.Hash;
using shop.Services.Kdf;
using shop.Services.Upload;

namespace shop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews(); // добавляем сервисы MVC
            builder.Services.AddSingleton<IHashService, ShaHashService>();
            builder.Services.AddSingleton<IKdfService, Pbkdf1Service>();

            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MsSql")),
                ServiceLifetime.Singleton
            );

            builder.Services.AddSingleton<DataAccessor>();
            builder.Services.AddSingleton<IUploadServise, UploadServiceV1>();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();
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
            app.UseSession();
            app.UseSessionAuth();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
        }
    }
}
