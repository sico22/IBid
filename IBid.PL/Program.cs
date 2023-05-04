using Microsoft.AspNetCore.Authentication.Cookies;
using IBid.DAL.DataContext;
using IBid.DAL.Repositories;
using IBid.DAL.Repositories.Contracts;
using IBid.BLL.Services;
using IBid.BLL.Services.Contracts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option => {
        option.LoginPath = "/Access/AdminLogin";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);

    });
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<IbidContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Sql"));
});

builder.Services.AddTransient(typeof(IAuthenticationRepository<>), typeof(AuthenticationRepository<>));
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient(typeof(IAdminRepository<>), typeof(AdminRepository<>));
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddTransient(typeof(IVolunteerRepository<>), typeof(VolunteerRepository<>));
builder.Services.AddScoped<IVolunteerService, VolunteerService>();
builder.Services.AddTransient(typeof(IItemRepository<>), typeof(ItemRepository<>));
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddTransient(typeof(IBidRepository<>), typeof(BidRepository<>));
builder.Services.AddScoped<IBidService, BidService>();
builder.Services.AddTransient(typeof(IBidHistoryRepository<>), typeof(BidHistoryRepository<>));
builder.Services.AddScoped<IBidHistoryService, BidHistoryService>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Admin/Error");
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
    pattern: "{controller=Access}/{action=AdminLogin}/{id?}");

app.Run();
