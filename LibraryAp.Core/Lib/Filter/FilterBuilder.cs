using Lib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lib {
    internal class FilterBuilder {

        readonly MethodInfo stringContainsMethod = typeof(string).GetMethod("Contains");
        readonly MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
        readonly MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });

        public readonly Dictionary<eBooleanOperatorType, Func<Expression, Expression, Expression, Expression>> Expressions;

        internal FilterBuilder() {

            Expressions = new Dictionary<eBooleanOperatorType, Func<Expression, Expression, Expression, Expression>>
            {
                { eBooleanOperatorType.EqualTo, (member, constant, constant2) => Expression.Equal(member, constant) },
                { eBooleanOperatorType.NotEqualTo, (member, constant, constant2) => Expression.NotEqual(member, constant) },
                { eBooleanOperatorType.GreaterThan, (member, constant, constant2) => Expression.GreaterThan(member, constant) },
                { eBooleanOperatorType.GreaterThanOrEqualTo, (member, constant, constant2) => Expression.GreaterThanOrEqual(member, constant) },
                { eBooleanOperatorType.LessThan, (member, constant, constant2) => Expression.LessThan(member, constant) },
                { eBooleanOperatorType.LessThanOrEqualTo, (member, constant, constant2) => Expression.LessThanOrEqual(member, constant) },
                { eBooleanOperatorType.Contains, (member, constant, constant2) => Contains(member, constant) },
                { eBooleanOperatorType.StartsWith, (member, constant, constant2) => Expression.Call(member, startsWithMethod, constant) },
                { eBooleanOperatorType.EndsWith, (member, constant, constant2) => Expression.Call(member, endsWithMethod, constant) },
                { eBooleanOperatorType.Between, (member, constant, constant2) => Between(member, constant, constant2) },
                { eBooleanOperatorType.In, (member, constant, constant2) => Contains(member, constant) },
                { eBooleanOperatorType.IsNull, (member, constant, constant2) => Expression.Equal(member, Expression.Constant(null)) },
                { eBooleanOperatorType.IsNotNull, (member, constant, constant2) => Expression.NotEqual(member, Expression.Constant(null)) },
                { eBooleanOperatorType.IsEmpty, (member, constant, constant2) => Expression.Equal(member, Expression.Constant(String.Empty)) },
                { eBooleanOperatorType.IsNotEmpty, (member, constant, constant2) => Expression.NotEqual(member, Expression.Constant(String.Empty)) },
                { eBooleanOperatorType.IsNullOrWhiteSpace, (member, constant, constant2) => IsNullOrWhiteSpace(member) },                
                { eBooleanOperatorType.IsNotNullNorWhiteSpace, (member, constant, constant2) => IsNotNullNorWhiteSpace(member) }
            };

        }

        public readonly MethodInfo trimMethod = typeof(string).GetMethod("Trim", new Type[0]);
        public readonly MethodInfo toLowerMethod = typeof(string).GetMethod("ToLower", new Type[0]);

        public Expression GetMemberExpression(Expression param, string propertyName) {
            if (propertyName.Contains(".")) {
                int index = propertyName.IndexOf(".");
                var subParam = Expression.Property(param, propertyName.Substring(0, index));
                return GetMemberExpression(subParam, propertyName.Substring(index + 1));
            }

            return Expression.Property(param, propertyName);
        }

        public Expression<Func<T, bool>> GetExpression<T>(IFilter filter) where T : class {
            var param = Expression.Parameter(typeof(T), "x");
            Expression expression = null;
            var connector = eLogicOperatorType.And;
            foreach (var statement in filter.Statements) {
                Expression expr = null;
                if (IsList(statement))
                    expr = ProcessListStatement(param, statement);
                else
                    expr = GetExpression(param, statement);

                expression = expression == null ? expr : CombineExpressions(expression, expr, connector);
                connector = statement.LogicOperator;
            }

            expression = expression ?? Expression.Constant(true);

            return Expression.Lambda<Func<T, bool>>(expression, param);
        }

        private bool IsList(IFilterStatement statement) {
            return statement.PropertyName.Contains("[") && statement.PropertyName.Contains("]");
        }

        private Expression CombineExpressions(Expression expr1, Expression expr2, eLogicOperatorType connector) {
            return connector == eLogicOperatorType.And ? Expression.AndAlso(expr1, expr2) : Expression.OrElse(expr1, expr2);
        }

        private Expression ProcessListStatement(ParameterExpression param, IFilterStatement statement) {
            var basePropertyName = statement.PropertyName.Substring(0, statement.PropertyName.IndexOf("["));
            var propertyName = statement.PropertyName.Replace(basePropertyName, "").Replace("[", "").Replace("]", "");

            var type = param.Type.GetProperty(basePropertyName).PropertyType.GetGenericArguments()[0];
            ParameterExpression listItemParam = Expression.Parameter(type, "i");
            var lambda = Expression.Lambda(GetExpression(listItemParam, statement, propertyName), listItemParam);
            var member = GetMemberExpression(param, basePropertyName);
            var enumerableType = typeof(Enumerable);
            var anyInfo = enumerableType.GetMethods(BindingFlags.Static | BindingFlags.Public).First(m => m.Name == "Any" && m.GetParameters().Count() == 2);
            anyInfo = anyInfo.MakeGenericMethod(type);
            return Expression.Call(anyInfo, member, lambda);
        }

        private Expression GetExpression(ParameterExpression param, IFilterStatement statement, string propertyName = null) {
            Expression resultExpr = null;
            var memberName = propertyName ?? statement.PropertyName;
            Expression member = GetMemberExpression(param, memberName);
            Expression constant = GetConstantExpression(member, statement.Value);
            Expression constant2 = GetConstantExpression(member, statement.Value2);

            if (Nullable.GetUnderlyingType(member.Type) != null && statement.Value != null) {
                resultExpr = Expression.Property(member, "HasValue");
                member = Expression.Property(member, "Value");
            }

            var safeStringExpression = GetSafeStringExpression(member, statement.BooleanOperator, constant, constant2);
            resultExpr = resultExpr != null ? Expression.AndAlso(resultExpr, safeStringExpression) : safeStringExpression;
            resultExpr = GetSafePropertyMember(param, memberName, resultExpr);

            if ((statement.BooleanOperator == eBooleanOperatorType.IsNull || statement.BooleanOperator == eBooleanOperatorType.IsNullOrWhiteSpace) && memberName.Contains(".")) {
                resultExpr = Expression.OrElse(CheckIfParentIsNull(param, member, memberName), resultExpr);
            }

            return resultExpr;
        }

        private Expression GetSafeStringExpression(Expression member, eBooleanOperatorType operation, Expression constant, Expression constant2) {
            if (member.Type != typeof(string)) {
                return Expressions[operation].Invoke(member, constant, constant2);
            }

            Expression newMember = member;

            if (operation != eBooleanOperatorType.IsNullOrWhiteSpace && operation != eBooleanOperatorType.IsNotNullNorWhiteSpace) {
                var trimMemberCall = Expression.Call(member, trimMethod);
                newMember = Expression.Call(trimMemberCall, toLowerMethod);
            }

            Expression resultExpr = operation != eBooleanOperatorType.IsNull ?
                                    Expressions[operation].Invoke(newMember, constant, constant2) :
                                    Expressions[operation].Invoke(member, constant, constant2);

            if (member.Type == typeof(string) && operation != eBooleanOperatorType.IsNull) {
                if (operation != eBooleanOperatorType.IsNullOrWhiteSpace && operation != eBooleanOperatorType.IsNotNullNorWhiteSpace) {
                    Expression memberIsNotNull = Expression.NotEqual(member, Expression.Constant(null));
                    resultExpr = Expression.AndAlso(memberIsNotNull, resultExpr);
                }
            }

            return resultExpr;
        }

        public Expression GetSafePropertyMember(ParameterExpression param, String memberName, Expression expr) {
            if (!memberName.Contains(".")) {
                return expr;
            }

            string parentName = memberName.Substring(0, memberName.IndexOf("."));
            Expression parentMember = GetMemberExpression(param, parentName);
            return Expression.AndAlso(Expression.NotEqual(parentMember, Expression.Constant(null)), expr);
        }

        private Expression CheckIfParentIsNull(Expression param, Expression member, string memberName) {
            string parentName = memberName.Substring(0, memberName.IndexOf("."));
            Expression parentMember = GetMemberExpression(param, parentName);
            return Expression.Equal(parentMember, Expression.Constant(null));
        }

        private Expression GetConstantExpression(Expression member, object value) {
            if (value == null) return null;

            Expression constant = Expression.Constant(value);

            if (value is string) {
                var trimConstantCall = Expression.Call(constant, trimMethod);
                constant = Expression.Call(trimConstantCall, toLowerMethod);
            }

            return constant;
        }

        #region Operations 
        private Expression Contains(Expression member, Expression expression) {
            MethodCallExpression contains = null;
            if (expression is ConstantExpression constant && constant.Value is IList && constant.Value.GetType().IsGenericType) {
                var type = constant.Value.GetType();
                var containsInfo = type.GetMethod("Contains", new[] { type.GetGenericArguments()[0] });
                contains = Expression.Call(constant, containsInfo, member);
            }

            return contains ?? Expression.Call(member, stringContainsMethod, expression); ;
        }

        private Expression Between(Expression member, Expression constant, Expression constant2) {
            var left = Expressions[eBooleanOperatorType.GreaterThanOrEqualTo].Invoke(member, constant, null);
            var right = Expressions[eBooleanOperatorType.LessThanOrEqualTo].Invoke(member, constant2, null);

            return CombineExpressions(left, right, eLogicOperatorType.And);
        }

        private Expression IsNullOrWhiteSpace(Expression member) {
            Expression exprNull = Expression.Constant(null);
            var trimMemberCall = Expression.Call(member, trimMethod);
            Expression exprEmpty = Expression.Constant(string.Empty);
            return Expression.OrElse(
                                    Expression.Equal(member, exprNull),
                                    Expression.Equal(trimMemberCall, exprEmpty));
        }

        private Expression IsNotNullNorWhiteSpace(Expression member) {
            Expression exprNull = Expression.Constant(null);
            var trimMemberCall = Expression.Call(member, trimMethod);
            Expression exprEmpty = Expression.Constant(string.Empty);
            return Expression.AndAlso(
                                    Expression.NotEqual(member, exprNull),
                                    Expression.NotEqual(trimMemberCall, exprEmpty));
        }
        #endregion
    }
}
