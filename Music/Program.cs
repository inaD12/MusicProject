using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Music.Controllers;
using Music.Data;
using Music.Data.DataModels;
using Music.Services;
using Music.Services.Interfaces;
using SpotifyAPI.Web;

public class Program
{
    public static async Task Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.AddTransient<SongService>();
        builder.Services.AddTransient<ArtistService>();
        builder.Services.AddTransient<AlbumService>();
		builder.Services.AddTransient<ApiService>();
		builder.Services.AddHttpClient<ApiService>();
		builder.Services.AddControllersWithViews();
        builder.Services.AddLogging();
		builder.Services.AddScoped<IApiService, ApiService>();

		var app = builder.Build();



        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();

        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new[] { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        using (var scope = app.Services.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            string email = "admin@gmail.com";
            string password = "Admin1234<";

            string email2 = "user1@gmail.com";
            string password2 = "User1234<";

            string email3 = "user2@gmail.com";
            string password3 = "User12345<";

            if (await userManager.FindByEmailAsync(email) == null)
            {
                var admin = new User();
                admin.UserName = email;
                admin.Email = email;
                admin.EmailConfirmed = true;

                await userManager.CreateAsync(admin, password);

                await userManager.AddToRoleAsync(admin, "Admin");
            }

			if (await userManager.FindByEmailAsync(email2) == null)
			{
				var user = new User();
				user.UserName = email2;
				user.Email = email2;
				user.EmailConfirmed = true;

				await userManager.CreateAsync(user, password2);

				await userManager.AddToRoleAsync(user, "User");
			}

			if (await userManager.FindByEmailAsync(email3) == null)
			{
				var user = new User();
				user.UserName = email3;
				user.Email = email3;
				user.EmailConfirmed = true;

				await userManager.CreateAsync(user, password3);

				await userManager.AddToRoleAsync(user, "User");
			}

		}

        app.Run();
    }
}
