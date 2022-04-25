using Pay.CrossCutting;
using Pay.Infra.Queue;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .RegisterRepositories(builder.Configuration.GetConnectionString("DefaultConnection"))
    .RegisterMessaging(builder.Configuration.GetSection("RabbitMQ").Get<QueueOptions>())
    .RegisterServices();

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
