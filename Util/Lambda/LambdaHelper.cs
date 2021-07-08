using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Util
{
    public static class LambdaHelper
    {
        /// <summary>
        /// BuildPropertySpecifier
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="?"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        public static Expression<Func<T, TValue>> BuildPropertySpecifier<T, TValue>(string property)
        {
            if (string.IsNullOrWhiteSpace(property))
                return null;

            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "f");
            Expression expr = arg;
            foreach (string prop in props)
            {
                PropertyInfo pi = type.GetProperty(prop);
                if (pi == null)
                    continue;
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }

            var expression = Expression.Lambda(
                typeof(Func<T, TValue>),
                expr,
                arg
                );

            return (Expression<Func<T, TValue>>)expression;
        }

        /// <summary>
        /// BuildPropertySpecifier
        /// </summary>
        /// <param name="type"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static Expression BuildPropertySpecifier(Type type, string property)
        {
            if (string.IsNullOrWhiteSpace(property))
                return null;

            string[] props = property.Split('.');
            ParameterExpression arg = Expression.Parameter(type, "f");
            Expression expr = arg;
            foreach (string prop in props)
            {
                PropertyInfo pi = type.GetProperty(prop);
                if (pi == null)
                    continue;
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }

            var expression = Expression.Lambda(
                expr,
                arg
                );

            return expression;
        }
    }
}
