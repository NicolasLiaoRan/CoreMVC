using Core_MVC.Data;
using Core_MVC.Model;
using Core_MVC.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;

namespace Core_MVC
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //把EFCore配置成服务
            //var connectionString = _configuration["ConnectionStrings:SqlServerConnection"];
            var connectionString = _configuration.GetConnectionString("SqlServerConnection");
            services.AddDbContext<DataContext>(options=> {
                options.UseSqlServer(connectionString);
            });
            //services.AddSingleton<IRepository<Student>, InMemoryRepository>();
            services.AddScoped<IRepository<Student>, EFCoreRepository>();

            //把Identity配置成服务
            services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("SqlServerConnection"), b => b.MigrationsAssembly("Core MVC")));
            //注册Identity服务
            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<IdentityDbContext>();
            //配置输入条件
            services.Configure<IdentityOptions>(options =>
            {
                //password settings
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 1;
                //lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                //user settings
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopgrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }
            app.UseStaticFiles();
            //伺服node_modules文件夹以使用前端库
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/node_modules",
                FileProvider=new PhysicalFileProvider(Path.Combine(env.ContentRootPath,"node_modules"))
            });
            app.UseAuthentication();
            //app.UseStaticFiles("/node_modules");
            app.UseMvc(builder =>
            {
                builder.MapRoute("Default", "{Controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
