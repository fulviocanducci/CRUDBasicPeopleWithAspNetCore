using API.DataAccess;
using Microsoft.EntityFrameworkCore;
namespace API
{
   public class Program
   {
      private static string GetConnectionMySqlString(WebApplicationBuilder builder)
      {
         return builder.Configuration.GetConnectionString("DatabaseMySql");
      }
      private static MySqlServerVersion GetMysqlVersion()
      {
         return new MySqlServerVersion(new Version(8, 0, 31));
      }

      public static void Main(string[] args)
      {
         var builder = WebApplication.CreateBuilder(args);
         builder.Services.Configure<RouteOptions>(options =>
         {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
         });
         builder.Services.AddDbContext<APIContext>(options =>
         {
            options.UseMySql(GetConnectionMySqlString(builder), GetMysqlVersion());
         });
         builder.Services.AddControllers();         
         builder.Services.AddEndpointsApiExplorer();
         builder.Services.AddSwaggerGen();
         var app = builder.Build();
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