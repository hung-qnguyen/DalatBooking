using System.Text.Json.Serialization;
using DotNetEd.CoreAdmin;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NToastNotify;
using BookingApp.Data;
using BookingApp.Repository;
using BookingApp.Repository.IRepository;
using BookingApp.Utilities;
using BookingApp.Validations;

var builder = WebApplication.CreateBuilder(args);

//Mặc định sử dụng SQLite
builder.Services.AddDbContext<HotelContext>(
    options =>
        options.UseSqlite(
            builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "Connection string 'DefaultConnection' not found."
                )
        )
);

// //Gỡ comment dòng code này và Comment dòng code trên để sử dụng MySQL
// builder.Services.AddDbContext<HotelContext>(
//     options =>
//         options.UseMySql(
//             builder.Configuration.GetConnectionString("MySqlConnection"),
//             ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySqlConnection"))
//         )
// );

builder.Services
    .AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        // Password settings.
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredUniqueChars = 0;

        // options.User.AllowedUserNameCharacters =
        //     "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = true;
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<HotelContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

builder.Services.AddValidatorsFromAssemblyContaining<RoomTypeValidator>();

builder.Services
    .AddRazorPages()
    .AddNToastNotifyToastr(new ToastrOptions { ProgressBar = true, TimeOut = 5000 });

builder.Services.AddCoreAdmin("Admin");
builder.Services.AddCoreAdmin(
    new CoreAdminOptions()
    {
        IgnoreEntityTypes = new List<Type>()
        {
            typeof(IdentityUserToken<string>),
            typeof(IdentityUserRole<string>),
            typeof(IdentityUserClaim<string>),
            typeof(IdentityRoleClaim<string>),
            typeof(IdentityUserLogin<string>)
        }
    }
);

// Add services to the container.

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IEmailSender, EmailSender>();

// builder.Services.AddMvc()
//         .AddJsonOptions(
//             options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
//         );

// builder.Services.AddControllersWithViews();

builder.Services
    .AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

var app = builder.Build();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapDefaultControllerRoute();

app.UseNToastNotify();

app.UseCoreAdminCustomUrl("admin");
app.UseCoreAdminCustomTitle("Administrator");
SeedData.EnsurePopulated(app);

app.Run();
