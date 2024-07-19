using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Configuration;
using TodoApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("carpet_content")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseFileServer(new FileServerOptions()
{
    FileProvider = new PhysicalFileProvider("C:/Hali"),
    RequestPath = new PathString(),
    EnableDirectoryBrowsing = false // you make this true or false.
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
