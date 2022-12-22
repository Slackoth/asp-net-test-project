using ATP.Business.Employee;
using ATP.DataAccess;
using ATP.DataAccess.Repository.Employee;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container (Dependency injection).
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
// Providing logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// Providing employee data access layer
builder.Services.AddScoped<EmployeeRepository, EmployeeRepositoryImpl>();
// Providing employee business layer
builder.Services.AddScoped<BEmployee, BEmployeeImpl>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
