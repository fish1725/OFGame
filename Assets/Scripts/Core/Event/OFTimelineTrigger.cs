#region

using System.Collections.Generic;
using System.Linq;

#endregion

namespace Assets.Scripts.Core.Event {
    public class OFTimelineTrigger : IOFTrigger {
        #region Fields

        public List<OFAction> actions;
        public List<OFCondition> conditions;
        public float normalizedTime;

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