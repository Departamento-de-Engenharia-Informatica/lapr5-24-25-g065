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
using dddsample1.domain;
using DDDSample1.Infrastructure.OperationTypes;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5000", "https://localhost:5001");

// Add services to dependency scope.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<StaffService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<OperationRequestService>();
builder.Services.AddScoped<OperationTypeService>();

// Add Repositories to dependency scope
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOperationRequestRepository,OperationRequestRepository>();
builder.Services.AddScoped<IOperationTypeRepository,OperationTypeRepository>();

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
.AddCookie(options => {

                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            })
.AddGoogle(GoogleDefaults.AuthenticationScheme, options => 
{
    options.ClientId = builder.Configuration["GoogleKeys:ClientId"];
    options.ClientSecret = builder.Configuration["GoogleKeys:ClientSecret"];
});

// Add other services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhostAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        }
    );
    options.AddPolicy("AllowSwaggerUI",
        policy =>
        {
            policy.WithOrigins("http://localhost:5000", "https://localhost:5001")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials(); // if using authentication
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

app.UseHttpsRedirection();
app.UseRouting(); // Ensure routing is in place before authentication
app.UseCors("AllowSwaggerUI");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.UseAuthentication(); // Enable authentication
app.UseAuthorization(); // Enable authorization

// Configure endpoints
app.MapControllers(); // Ensures that your controllers are mapped correctly

// Set default route for MVC (if needed)



app.Run();
