using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagementApplication.Data;
using TaskManagementApplication.Repository;
using TaskManagementDomain.Entity;
using TaskManagementDomain.IRepository;
using TaskManagementSystemApi.Helper;
using TaskManagementSystemApi.IService;
using TaskManagementSystemApi.Seeds;
using TaskManagementSystemApi.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IProjectService, ProjectService>();  
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)).BuildServiceProvider();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionString")));
builder.Services.AddScoped<UserManager<AppUser>>();
builder.Services.AddScoped<SignInManager<AppUser>>();
builder.Services.AddIdentity<AppUser, IdentityRole> (
     options =>
     {
         options.Password.RequireUppercase = true; // on production add more secured options
         options.Password.RequireDigit = true;
         //options.SignIn.RequireConfirmedEmail = true;
     }
    ).AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAutoMapper(typeof(MapInitializer));
builder.Services.AddLogging();

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

SeedDatas.Seed(app);

app.Run();
