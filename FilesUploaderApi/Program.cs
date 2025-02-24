using FilesUploaderApi.Messaging;
using FilesUploaderApi.Middleware;
using FilesUploaderApi.Repository;
using FilesUploaderApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IRepository, MockRepository>();
builder.Services.AddTransient<IMessenger, ConsoleMessenger>();
builder.Services.AddScoped<IUploadService, UploadService>();
builder.Services.AddScoped<ICustomerSessionService, CustomerSessionService>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();
app.Run();

