using FilesUploaderApi.Messaging;
using FilesUploaderApi.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IRepository, MockRepository>();
builder.Services.AddTransient<IMessenger, ConsoleMessenger>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.MapControllers();
app.Run();

