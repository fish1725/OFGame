#region

using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Assets.Scripts.Core.Game;

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

		[OFExpression("CreateLight", "Create a light with name <color='red'>Light</color>")]
		public static void CreateLight(string name) {
			OFGame.Scene.CreateLight ();
		}
		
		[OFExpression("CreateLight2", "Create a light with name <color=#0000ffff><a href=OnClick>Light</a></color>")]
		public static void CreateLight2(string name) {
			OFGame.Scene.CreateLight ();
		}

        #endregion
    }
}