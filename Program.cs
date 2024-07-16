using CRUD.frontend.Clients;
using CRUD.frontend.Components;
using Microsoft.AspNetCore.Authentication.Cookies;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();


var ExerciseURl = builder.Configuration["ExerciseUrl"] ??
    throw new Exception("ExerciseUrl is not found");

builder.Services.AddHttpClient<PeopleClient>(client => client.BaseAddress = new Uri(ExerciseURl));
builder.Services.AddHttpClient<ExperiencesClient>(client => client.BaseAddress = new Uri(ExerciseURl));

builder.Services
    .AddBlazorise( options =>
    {
        options.Immediate = true;
    } )
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

///app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
