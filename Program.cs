using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.Extensions.Hosting;
using MySql.Data.EntityFrameworkCore.Extensions;
using DDDSample1.Infrastructure;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using DDDSample1.Domain.Patients;
using DDDSample1.Infrastructure.Patients;
using DDDSample1.Domain.Specializations;
using DDDSample1.Domain.Staffs;
using DDDSample1.Domain.Users;
using DDDSample1.Domain.Passwords;
using DDDSample1.Infrastructure.OperationTypes;
using DDDSample1.Infrastructure.Specializations;
using DDDSample1.Infrastructure.Staffs;
using DDDSample1.Infrastructure.Users;
//using DDDSample1.Infrastructure.Passwords;
using DDDNetCore.IRepos;

var builder = WebApplication.CreateBuilder(args);

// Add services to dependency scope.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<OperationTypeService>();
builder.Services.AddScoped<SpecializationService>();
builder.Services.AddScoped<StaffService>();
builder.Services.AddScoped<UserService>();
//builder.Services.AddScoped<PasswordService>();

// Add Repositories to dependency scope
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IOperationTypeRepository, OperationTypeRepository>();
builder.Services.AddScoped<ISpecializationRepository, SpecializationRepository>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IUserRepository,UserRepository>();
//builder.Services.AddScoped<IPasswordRepository,PasswordRepository>();


// Configure MySQL as the database provider
builder.Services.AddDbContext<DDDSample1DbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("UserContext"),
        new MySqlServerVersion(new Version(8, 0, 39))
    )
);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
    .AddCookie()
    .AddGoogle(GoogleDefaults.AuthenticationScheme, options => 
    {
        options.ClientId=builder.Configuration.GetSection("GoogleKeys:ClientId").Value;
        options.ClientSecret=builder.Configuration.GetSection("GoogleKeys:ClientSecret").Value;
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
app.UseAuthentication(); // Enable authentication
app.UseRouting();
app.UseAuthorization(); // Enable authorization
app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            }
);

app.MapControllers();

app.Run();

