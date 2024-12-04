using ElasticSearch.API.Extensions;
using ElasticSearch.API.Models;
using ElasticSearch.API.Repositories;
using ElasticSearch.API.Services;
using System.Text.Json.Serialization;

namespace ElasticSearch.API {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers()
            .AddJsonOptions(options => {
                // Enum değerlerini string olarak serileştir
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            builder.Services.AddElasticSearchExt(builder.Configuration);
            builder.Services.AddScoped<ProductRepository>();
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddScoped<ECommerceRepository>();
            builder.Services.AddScoped<BlogsRepository>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.MapControllers();
            app.UseAuthorization();

            app.Run();
        }
    }
}
