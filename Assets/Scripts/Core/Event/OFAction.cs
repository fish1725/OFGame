#region

using System;
using System.Linq.Expressions;

#endregion

namespace Assets.Scripts.Core.Event {
    public class OFAction {
        #region Fields

        public Expression expression;
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