using Microsoft.EntityFrameworkCore;
using TTMH_EDA_HIS.Data;

var builder = WebApplication.CreateBuilder(args);
var conn = builder.Configuration.GetConnectionString("HISDBContext");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<HisdbContext>(
    options => options.UseSqlServer(conn)
);

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
