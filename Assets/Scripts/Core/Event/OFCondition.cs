#region

using System;
using System.Linq.Expressions;

#endregion

namespace Assets.Scripts.Core.Event {
    public class OFCondition : IOFExpression {
        #region Fields

		public Expression expression { get; set; }
        private Func<bool> _compiled;

        #endregion

        #region Properties

        public bool result {
            get { return _compiled(); }
        }

        #endregion

        #region Methods

        public void Compile() {
            _compiled = (Func<bool>) Expression.Lambda(expression).Compile();
        }

        #endregion
    }
}