using Application.Abstractions;
using Application.Todos.Commands;
using Application.Todos.Queries;
using Infrastructure.Db;
using Infrastructure.Todos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IConnectionFactory, SqlConnectionFactory>();
builder.Services.AddScoped<ITodoReadDb, TodoReadDb>();
builder.Services.AddScoped<ITodoWriteDb, TodoWriteDb>();

builder.Services.AddScoped<CreateTodoHandler>();
builder.Services.AddScoped<UpdateTodoHandler>();
builder.Services.AddScoped<DeleteTodoHandler>();
builder.Services.AddScoped<GetTodoByIdHandler>();
builder.Services.AddScoped<ListTodosHandler>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



