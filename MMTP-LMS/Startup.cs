﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MMTP_LMS.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MMTP_LMS.Models;

namespace MMTP_LMS
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
            
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<Person, IdentityRole>()
               
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider servises)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Models.LmsActivity, ViewModels.StudentViewModel>();
               // cfg.CreateMap<ViewModels.StudentViewModel, Models.LmsActivity>();
            });
           CreateUserRoles(servises).Wait();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<Person>>();
            string[] roleNames = { "Admin", "Manager", "Member" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                // ensure that the role does not exist
                if (!roleExist)
                {
                    //create the roles and seed them to the database: 
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // find the user with the admin email 
            var _user = await UserManager.FindByEmailAsync("admin@admin.com");

            // check if the user exists
            if (_user == null)
            {
                //Here you could create the super admin who will maintain the web app
                var poweruser = new Person
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                   // CourseId = 1, // Temp for course 1 
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                };
                string adminPassword = "Lexicon!0";

                var createPowerUser = await UserManager.CreateAsync(poweruser, adminPassword);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the role
                    await UserManager.AddToRoleAsync(poweruser, "Admin");

                }
            }
           
        }
    }
}
