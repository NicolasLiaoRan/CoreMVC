using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core_MVC.Data;
using Core_MVC.Model;
using Core_MVC.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

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
            //app.UseStaticFiles("/node_modules");
            app.UseMvc(builder =>
            {
                builder.MapRoute("Default", "{Controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
