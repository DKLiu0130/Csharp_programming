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

            // ����MySQL���ݿ�������
            builder.Services.AddDbContext<OrderDbContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("OrderDb"),
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("OrderDb"))
                )
            );

            // ע������
            builder.Services.AddScoped<OrderService>();

            // ���ÿ�������JSON���л�
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler =
                        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                });

            // ����Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "��������ϵͳ API",
                    Version = "v1"
                });
                c.EnableAnnotations();
                c.CustomOperationIds(apiDesc =>
                    apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null);
            });

            var app = builder.Build();

            // �Զ�Ǩ�����ݿ�
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
                dbContext.Database.Migrate();
            }

            // ���ÿ��������м��
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "����ϵͳ v1");
                    c.RoutePrefix = "swagger";
                });
            }

            // ���������м��
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}