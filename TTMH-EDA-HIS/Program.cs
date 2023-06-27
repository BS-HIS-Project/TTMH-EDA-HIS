using Microsoft.EntityFrameworkCore;
using HISDB.Data;
using TTMH_EDA_HIS.Interfaces;
using TTMH_EDA_HIS.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var conn = builder.Configuration.GetConnectionString("HISDBContext");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<HisdbContext>(
    options => options.UseSqlServer(conn)
);
builder.Services.AddSingleton<IHashService,SHA512HashService>();
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.Name = "TTMH_EDA_HIS_LOGIN";
    options.ExpireTimeSpan=TimeSpan.FromMinutes(45);
    options.SlidingExpiration = true;
    options.AccessDeniedPath = "/Account/Denied";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();

    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "HtmlPages")),
    RequestPath = "/HtmlPages"
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
