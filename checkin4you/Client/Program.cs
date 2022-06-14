using checkin4you.Client;
using checkin4you.Client.Services.Implementations;
using checkin4you.Client.Services.Interfaces;
using checkin4you.Client.Services.States;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddSingleton<LanguageStateService>();
builder.Services.AddSingleton<ReservationStateService>();

await builder.Build().RunAsync();
