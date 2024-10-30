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
using DDDSample1.Domain.Specializations;
using DDDSample1.Domain.Staffs;
using DDDSample1.Domain.Users;
using DDDSample1.Infrastructure.Specializations;
using DDDSample1.Infrastructure.Staffs;
using DDDSample1.Infrastructure.Users;
using DDDNetCore.IRepos;
using DDDNetCore.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to dependency scope.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<SpecializationService>();
builder.Services.AddScoped<StaffService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<OperationRequestService>();
builder.Services.AddScoped<OperationTypeService>();

// Add Repositories to dependency scope
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<ISpecializationRepository, SpecializationRepository>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

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
.AddCookie()
.AddGoogle(GoogleDefaults.AuthenticationScheme, options => 
{
    options.ClientId = builder.Configuration["GoogleKeys:ClientId"];
    options.ClientSecret = builder.Configuration["GoogleKeys:ClientSecret"];
});

// Add other services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting(); // Ensure routing is in place before authentication

app.UseAuthentication(); // Enable authentication
app.UseAuthorization(); // Enable authorization

// Configure endpoints
app.MapControllers(); // Ensures that your controllers are mapped correctly

// Set default route for MVC (if needed)
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
