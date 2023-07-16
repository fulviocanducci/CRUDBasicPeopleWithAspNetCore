using API.Models;
using API.Models.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;

namespace API.DataAccess
{
   public class APIContext : DbContext
   {
      public APIContext(DbContextOptions options) : base(options)
      {
      }

      public DbSet<People>? Peoples { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.HasCharSet("utf8mb4");
         modelBuilder.UseCollation("utf8mb4_0900_ai_ci");

         modelBuilder.ApplyConfiguration(new PeopleMapping());

         #region TranslateFunctionMysql
         //modelBuilder.HasDbFunction(typeof(DbMySqlFunctions).GetMethod(nameof(DbMySqlFunctions.Like)), x =>
         //{
         //   x.HasName("LIKE").IsBuiltIn(true);
         //   x.
         //   x.HasParameter("value").PropagatesNullability();
         //   x.IsNullable(true);
         //});
         #endregion
      }
   }

   public static class APIContextIQueryableFunctions
   {
      public static IQueryable<T> WhereContains<T, TProperty>(this IQueryable<T> model, Expression<Func<T, TProperty>> property, string value) where T : class, new()
      {
         var propertyName = ((MemberExpression)property.Body).Member.Name;
         return model.Where($"{propertyName}.Contains(@0)", $"{value}");
      }

      //public static IQueryable<T> WhereContains<T, TProperty>(this IQueryable<T> model, Expression<Func<T, TProperty>> property, string value) where T: class, new()
      //{
      //   var propertyName = ((MemberExpression)property.Body).Member.Name;
      //   var item = Expression.Parameter(typeof(T), "item");
      //   var selector = Expression.PropertyOrField(item, propertyName);
      //   Expression.Lambda<Func<T, bool>>()
      //   Expression<Func<T, bool>> where = c => EF.Functions.Like(selector, $"%{value}%");
      //   return model.Where<T>(where);
      //}

      //public static IQueryable<Tsource> Where<Tsource, Tproperty>(this IQueryable<Tsource> source, Expression<Func<Tsource, Tproperty>> property, IList<int> accpetedValues)
      //{
      //   var propertyName = ((MemberExpression)property.Body).Member.Name;

      //   if (accpetedValues == null || !accpetedValues.Any() || string.IsNullOrEmpty(propertyName))
      //      return source;

      //   var item = Expression.Parameter(typeof(Tsource), "item");
      //   var selector = Expression.PropertyOrField(item, propertyName);
      //   var predicate = Expression.Lambda<Func<Tsource, bool>>(
      //       Expression.Call(typeof(Enumerable), "Contains", new[] { typeof(Tproperty) },
      //           Expression.Constant(accpetedValues), selector),
      //       item);
      //   return source.Where(predicate);
      //}

      //public static IQueryable<TSource> WhereContains<TSource, TResult>(this IQueryable<TSource> query, Expression<Func<TSource, TResult>> column, IList<TResult> values)
      //{
      //   MethodInfo iListTResultContains = typeof(ICollection<>).MakeGenericType(typeof(TResult)).GetMethod("Contains", BindingFlags.Instance | BindingFlags.Public, null, new[] { typeof(TResult) }, null);

      //   var contains = Expression.Call(Expression.Constant(values), iListTResultContains, column.Body);

      //   var lambda = Expression.Lambda<Func<TSource, bool>>(contains, column.Parameters);

      //   return query.Where(lambda);
      //}


   }
}
