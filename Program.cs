using MFui;
using MFui.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


builder.Services.AddScoped<ISchemeListItemService, SchemeListItemService>();
builder.Services.AddScoped<INAVResponseService, NAVResponseService>();
builder.Services.AddScoped<ISchemeLatestNAVService, SchemeLatestNAVService>();
builder.Services.AddScoped<IMFLatestNAVService, MFLatestNAVService>();
builder.Services.AddScoped<ISearchListItemService, SearchListItemService>();

builder.Services.AddBlazorBootstrap();


await builder.Build().RunAsync();
