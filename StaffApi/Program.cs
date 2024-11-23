using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StaffApi.AutoMapping;
using StaffApi.Buisness;
using StaffApi.Buisness.UserCheck;
using StaffApi.Data;
using StaffApi.Data.Interfaces;
using StaffApi.Data.Repositories;
using StaffApi.Entities;
using StaffApi.Interfaces;
using StaffApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                ?? throw new InvalidOperationException("No connection string was found");

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseLazyLoadingProxies()
        .UseSqlServer(connectionString));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


builder.Services.AddTransient(typeof(IStudentRepository), typeof(StudentRepository));
builder.Services.AddTransient(typeof(IFamilyMemberRepository), typeof(FamilyMemberRepository));
builder.Services.AddScoped<IUserRoleCheck, UserRoleCheck>();
builder.Services.AddScoped<IMapping, Mapping>();
builder.Services.AddScoped<IEnumsCheck, EnumsCheck>();
 
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();
