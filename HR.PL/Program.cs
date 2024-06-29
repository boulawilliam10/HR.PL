using HR.BLL.Interface;
using HR.BLL.Repository;
using HR.DAL.Context;
using HR.DAL.Models;
using HR.PL.MapperProfiles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HR.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            #region Dependency Injection [DB - UnitofWork - Identity - Authentication - Mapping Profiles]
            //DI
            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
            });


            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
            }).AddEntityFrameworkStores<CompanyDbContext>()
          .AddDefaultTokenProviders();//Default Token



            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "Account/Login";
                    options.AccessDeniedPath = "Home/Error";
                });

            //Mapping Profiles
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles())); 
            #endregion

            //---------------------------------------------------------------------------

            var app = builder.Build();

			#region Update-Database Explicitly
			//Update-Database Explicitly
			var scope = app.Services.CreateScope(); //Services Scoped
			var services = scope.ServiceProvider; //DI
												  //LoggerFactory To Log Errors
			var LoggerFactory = services.GetRequiredService<ILoggerFactory>();

			try
			{
				var dbcontext = services.GetRequiredService<CompanyDbContext>(); //Ask CLR To Create Object From Store Context Explicitly
																				 //Apply Migrate If Exist - Update Database
				await dbcontext.Database.MigrateAsync();
			}
			catch (Exception ex)
			{
				var Logger = LoggerFactory.CreateLogger<Program>();
				Logger.LogError(ex, "An Error Occurred During Applying The Migration");
			}

			#endregion

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
