using Microsoft.EntityFrameworkCore;
using auth.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin() // 允许所有来源
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
// 注册数据库上下文
// 指定版本 11 是为了使用 Find、FirstOrDefault、Skip Task
// 参考：https://www.cnblogs.com/colorchild/p/12755638.html
builder.Services.AddDbContext<auth.Database.AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("oracle"), UseOracleOptions => UseOracleOptions.UseOracleSQLCompatibility("11")));



var app = builder.Build();

// Configure the HTTP request pipeline.
// 救命恩人：https://www.cnblogs.com/cxxtreasure/p/14332484.html
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
