using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Services;


var builder = WebApplication.CreateBuilder(args);

// Add DbContext (using SQLite here; swap to SqlServer if desired)
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlite("Data Source=todo.db"));

// Register the TodoService
builder.Services.AddScoped<TodoService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply any pending EF Core migrations and create DB
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TodoContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
