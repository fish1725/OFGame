#region

using System;
using System.Linq.Expressions;

#endregion

namespace Assets.Scripts.Core.Event {
    public class OFAction : IOFExpression {
        #region Fields

		public Expression expression { get; set; }
        private Action _compiled;

        #endregion

        #region Methods

        public void Compile() {
            _compiled = (Action) Expression.Lambda(expression).Compile();
        }

        public void Execute() {
            _compiled();
        }

        #endregion
    }
}