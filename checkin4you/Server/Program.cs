using checkin4you.Server.DataAccess;
using checkin4you.Server.Services.Implementations;
using checkin4you.Server.Services.Interfaces;
using checkin4you.Server.Settings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAny",
                      policy =>
                      {
                          policy.AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowAnyOrigin();
                      });
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AidaX_kleiner_ItalienerContext>(options => options.UseSqlServer("name=ConnectionStrings:Database"));
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IMailService, MailService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UsePathBase("/");
app.UseCors("AllowAny");

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.MapControllers();
app.MapDefaultControllerRoute();
app.MapFallbackToFile("index.html");

app.Run();
