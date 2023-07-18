using API.DataAccess;
using Microsoft.EntityFrameworkCore;
namespace API
{
   public class Program
   {
      #region private_methods
      private static string GetConnectionMySqlString(WebApplicationBuilder builder)
      {
         return builder.Configuration.GetConnectionString("DatabaseMySql") ?? string.Empty;
      }

      private static MySqlServerVersion GetMysqlVersion()
      {
         return new MySqlServerVersion(new Version(8, 0, 31));
      }
      #endregion private_methods

      public static void Main(string[] args)
      {
         var builder = WebApplication.CreateBuilder(args);
         builder.Services.AddMediatR(options => {
            options.RegisterServicesFromAssembly(typeof(Program).Assembly);
         });
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