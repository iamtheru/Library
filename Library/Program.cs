using Library;
using Library.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMvc();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddBookService();
builder.Services.AddRecomendedService();
builder.Services.AddRepository();
builder.Services.AddAutomapper();
builder.Services.AddValidation();
builder.Services.AddValidationRuls();
builder.Services.ConfigureConfig(builder);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseExceptionHandler("/api/error");

app.UseMiddleware<RequestLoggerMidleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
