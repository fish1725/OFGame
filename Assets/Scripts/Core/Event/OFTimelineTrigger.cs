#region

using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#endregion

namespace Assets.Scripts.Core.Event {
    public class OFTimelineTrigger : IOFTrigger {
        #region Fields
		
		[OFUIListItemCell(typeof(List<Expression>))]
		public List<OFAction> actions{ get; set; }
		[OFUIListItemCell(typeof(List<Expression>))]
		public List<OFCondition> conditions{ get; set; }
		[OFUIListItemCell(typeof(string))]
		public float normalizedTime{ get; set; }

        #endregion

        #region Constructors

        public OFTimelineTrigger() {
            normalizedTime = 0;
            conditions = new List<OFCondition>();
            actions = new List<OFAction>();
        }

        #endregion

        #region Methods

        public void Compile() {
            foreach (OFAction action in actions) {
                action.Compile();
            }
            foreach (OFCondition condition in conditions) {
                condition.Compile();
            }
        }

        public void Register(IOFEventDispatcher eventDispatcher) {
            eventDispatcher.On(normalizedTime, EventHandler);
        }

        private void EventHandler(params object[] args) {
            bool conditionResult = conditions.Aggregate(true, (current, condition) => current & condition.result);
            if (conditionResult) {
                foreach (OFAction action in actions) {
                    action.Execute();
                }
            }
        }

        #endregion
    }
}