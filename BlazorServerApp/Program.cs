using System.Net.Http.Headers;
using Blazored.LocalStorage;
using BlazorServerApp.Components;
using BlazorServerApp.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using MiApi.Models;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);


// Registra HttpClient con la URL base de tu API--------------
//pasamos la url de nuestra api local 
// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5026/") });
builder.Services.AddScoped(sp =>
{
    var client = new HttpClient { BaseAddress = new Uri("http://localhost:5026/") };
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "jwt_token");
    return client;
});


//añadimos
builder.Services.AddScoped<IUserServices,UserServices>();
builder.Services.AddScoped<IAppoinmentServices,AppoinmentServices>();

builder.Services.AddBlazoredLocalStorage();


builder.Services.AddSweetAlert2();
builder.Services.AddScoped<SweetAlertService>();
builder.Services.AddMudServices();


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//esto es para tener implementado o poder usar la api en el razor
builder.Services.AddHttpClient();

var app = builder.Build();

// Configura el enrutamiento de Blazor
app.UseRouting();
app.MapBlazorHub();
// app.MapFallbackToPage("/_Host");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
