#region

using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

#endregion

namespace Assets.Scripts.Core.Event {
    public class OFExpressionController {
        #region Methods

        public static Expression CreateConstantExpression(object value) {
            return Expression.Constant(value);
        }

        public static Expression CreateMethodCallExpression(Expression instance, MethodInfo method,
            List<Expression> arguments) {
            return Expression.Call(instance, method, arguments);
        }

        #endregion
    }
}