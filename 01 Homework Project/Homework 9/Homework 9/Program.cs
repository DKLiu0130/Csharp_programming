using Homework_9.Data;
using Homework_9.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Reflection;

namespace Homework_9
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 配置MySQL数据库上下文
            builder.Services.AddDbContext<OrderDbContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("OrderDb"),
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("OrderDb"))
                )
            );

            // 注册服务层
            builder.Services.AddScoped<OrderService>();

            // 配置控制器和JSON序列化
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler =
                        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                });

            // 配置Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "订单管理系统 API",
                    Version = "v1"
                });
                c.EnableAnnotations();
                c.CustomOperationIds(apiDesc =>
                    apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null);
            });

            var app = builder.Build();

            // 自动迁移数据库
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
                dbContext.Database.Migrate();
            }

            // 配置开发环境中间件
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "订单系统 v1");
                    c.RoutePrefix = "swagger";
                });
            }

            // 生产环境中间件
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}