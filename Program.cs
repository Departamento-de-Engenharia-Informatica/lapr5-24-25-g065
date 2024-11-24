using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using System;
using Microsoft.Extensions.Hosting;
using DDDSample1.Infrastructure;
using Microsoft.Extensions.Configuration;
using DDDSample1.Domain.Patients;
using DDDSample1.Infrastructure.Patients;
using DDDSample1.Domain.Staffs;
using DDDSample1.Domain.Users;
using DDDSample1.Infrastructure.Staffs;
using DDDSample1.Infrastructure.Users;
using DDDNetCore.IRepos;
using DDDNetCore.Services;
using DDDSample1.Domain;
using DDDSample1.Infrastructure.OperationTypes;
using Microsoft.AspNetCore.Http;
using dddsample1.domain;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5000", "https://localhost:5001");

// Add other services
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<StaffService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<OperationRequestService>();
builder.Services.AddScoped<OperationTypeService>();
builder.Services.AddScoped<AuthServicePatient>();

// Add Repositories to dependency scope
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOperationRequestRepository, OperationRequestRepository>();
builder.Services.AddScoped<IOperationTypeRepository, OperationTypeRepository>();

// Configure HttpClient and register AuthServicePatient
builder.Services.AddHttpClient<AuthServicePatient>();
builder.Services.AddScoped<AuthServicePatient>();

// Configure MySQL as the database provider
builder.Services.AddDbContext<DDDSample1DbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("UserContext"),
        new MySqlServerVersion(new Version(8, 0, 39))
    )
);

// Configure authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
})
.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
{
    options.ClientId = builder.Configuration["GoogleKeys:ClientId"];
    options.ClientSecret = builder.Configuration["GoogleKeys:ClientSecret"];
});

// Add authorization services
builder.Services.AddAuthorization();

// Add necessary services for controllers
builder.Services.AddControllers();

// Add Swagger and API Explorer
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS policies
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200","http://localhost:5000", "https://localhost:5001", "http://localhost:5001","https://localhost:5000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        }
    );
    
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseHttpsRedirection();
app.UseRouting(); // Ensure routing is in place before authentication

// Enable CORS for specific origins
app.UseCors("AllowSpecificOrigins");
app.UseAuthentication(); // Enable authentication
app.UseAuthorization(); // Enable authorization

// Map controllers
app.MapControllers(); // Ensures that your controllers are mapped correctly

// Set default route for MVC (if needed)
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
