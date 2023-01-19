using Microsoft.EntityFrameworkCore;
using ProvisionCase.BL.Abstract;
using ProvisionCase.BL.Concrete;
using ProvisionCase.DAL.Context;

namespace ProvisionCase.API
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddDbContext<ProvisionCaseDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("npgconstr")));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IExchangeRatesManager, ExchangeRatesManager>();
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
        }

    }
}