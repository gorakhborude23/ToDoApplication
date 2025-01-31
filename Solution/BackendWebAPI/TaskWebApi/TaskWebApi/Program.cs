using Microsoft.EntityFrameworkCore;
using TaskWebApi.Data;
using TaskWebApi.Mapper;
using TaskWebApi.Middleware;
using TaskWebApi.Repositories;
using TaskWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000", builder =>
        builder.WithOrigins("http://localhost:3000") // Allow only in dev
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("TaskManagementDb"));

builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAutoMapper(typeof(TaskMappingProfile));

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

// Apply CORS policy
app.UseCors("AllowLocalhost3000");

app.UseMiddleware<ExceptionHandlingMiddleware>();


app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated(); // Ensure the database is created
}


app.Run();
