using System;
using EAuction.Core.Entities;
using EAuction.Core.Repositories;
using EAuction.Core.Repositories.Abstractions;
using EAuction.Infrastructure.Data;
using EAuction.Infrastructure.Repositories;
using EAuction.Infrastructure.Repositories.Base;
using EAuction.UI.Clients;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EAuction.UI
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
            #region Database Configuration
            services.AddDbContext<WebAppContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));
            #endregion

            #region Identity Configuration
            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 4;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit = false;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<WebAppContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Home/Login";
                options.LogoutPath = $"/Home/Logout";
            });
            #endregion

            #region Project Configuration
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddMvc();
            services.AddRazorPages();
            services.AddSession(opt =>
            {
                opt.IdleTimeout = TimeSpan.FromMinutes(20);
            });
            #endregion

            #region Dependencies

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));

            services.AddHttpClient();
            services.AddHttpClient<ProductClient>();
            services.AddHttpClient<AuctionClient>();
            services.AddHttpClient<BidClient>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name:"default",
                    pattern:"{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
