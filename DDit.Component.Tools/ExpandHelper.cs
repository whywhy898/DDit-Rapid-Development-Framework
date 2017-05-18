using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Data.Entity;
using AutoMapper;
using System.Reflection;


namespace DDit.Component.Tools
{
   public static class ExpandHelper
    {
       /// <summary>
       /// 多字段排序
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="Sour"></param>
       /// <param name="SortExpression"></param>
       /// <param name="values"></param>
       /// <returns></returns>
       public static IQueryable<T> OrderBy<T>(this IQueryable<T> Sour, string SortExpression,params object[] values)
       {
           if (Sour == null)
               throw new ArgumentException("操作对象为null");
           return DynamicQueryable.OrderBy(Sour, SortExpression, values);
       }

       /// <summary>
       /// 分页封装
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="Sour"></param>
       /// <param name="skinCount"></param>
       /// <param name="maxResultCount"></param>
       /// <returns></returns>
       public static IQueryable<T> PageBy<T>(this IQueryable<T> Sour, int skinCount, int maxResultCount) {
           if (Sour == null)
               throw new ArgumentException("操作对象为null");
           return Sour.Skip(skinCount).Take(maxResultCount);
       }

       /// <summary>
       /// 判断筛选
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="Sour"></param>
       /// <param name="condition"></param>
       /// <param name="func"></param>
       /// <returns></returns>
       public static IQueryable<T> WhereIf<T>(this IQueryable<T> Sour,bool condition,Expression<Func<T,bool>> func){
            if (Sour == null)
               throw new ArgumentException("操作对象为null");
            return condition ? Sour.Where(func) : Sour;
       }

       /// <summary>
       /// 
       /// </summary>
       /// <typeparam name="TSource"></typeparam>
       /// <typeparam name="TKey"></typeparam>
       /// <param name="source"></param>
       /// <param name="keySelector"></param>
       /// <returns></returns>
       public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
       {
           HashSet<TKey> seenKeys = new HashSet<TKey>();
           foreach (TSource element in source)
           {
               if (seenKeys.Add(keySelector(element)))
               {
                   yield return element;
               }
           }
       }

       /// <summary>
       /// 排序
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="Sour"></param>
       /// <param name="SortExpression"></param>
       /// <param name="Direction"></param>
       /// <returns></returns>
       public static IQueryable<T> OrderSort<T>(this IQueryable<T> Sour, string SortExpression, string Direction)
       {
           string SortDirection = string.Empty;
           if (Direction == "asc")
               SortDirection = "OrderBy";
           else if (Direction == "desc")
               SortDirection = "OrderByDescending";
           ParameterExpression pe = Expression.Parameter(typeof(T), SortExpression);
           PropertyInfo pi = typeof(T).GetProperty(SortExpression);
           Type[] types = new Type[2];
           types[0] = typeof(T);
           types[1] = pi.PropertyType;
           Expression expr = Expression.Call(typeof(Queryable), SortDirection, types, Sour.Expression, Expression.Lambda(Expression.Property(pe, SortExpression), pe));
           IQueryable<T> query = Sour.Provider.CreateQuery<T>(expr);
           return query;
       }

       /// <summary>
       /// model映射
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="Sour"></param>
       /// <returns></returns>
       public static IQueryable<T> SelectByMapping<T>(this IQueryable<T> Sour) {
           if (Sour == null)
               throw new ArgumentException("操作对象为null");
           return Sour.ProjectToQueryable<T>();
       }

       public static Expression<Func<T, bool>> True<T>() { return f => true; }
       public static Expression<Func<T, bool>> False<T>() { return f => false; }
       private static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
       {
           // build parameter map (from parameters of second to parameters of first)  
           var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

           // replace parameters in the second lambda expression with parameters from the first  
           var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

           // apply composition of lambda expression bodies to parameters from the first expression   
           return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
       }

       /// <summary>
       /// 查询条件辅助
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="first"></param>
       /// <param name="second"></param>
       /// <returns></returns>
       public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
       {
           return first.Compose(second, Expression.And);
       }

       public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
       {
           return first.Compose(second, Expression.Or);
       }

   }

   public class ParameterRebinder : ExpressionVisitor
   {
       private readonly Dictionary<ParameterExpression, ParameterExpression> map;
       public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
       {
           this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
       }
       public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
       {
           return new ParameterRebinder(map).Visit(exp);
       }
       protected override Expression VisitParameter(ParameterExpression p)
       {
           ParameterExpression replacement;
           if (map.TryGetValue(p, out replacement))
           {
               p = replacement;
           }
           return base.VisitParameter(p);
       }
   }
}
