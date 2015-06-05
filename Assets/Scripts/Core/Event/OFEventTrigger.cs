#region

using System.Collections.Generic;
using System.Linq;

#endregion

namespace Assets.Scripts.Core.Event {
    public class OFEventTrigger : IOFTrigger {
        #region Fields

        public List<OFAction> actions;
        public List<OFCondition> conditions;
        public List<OFEventType> events;

        #endregion

        #region Constructors

        public OFEventTrigger() {
            actions = new List<OFAction>();
            conditions = new List<OFCondition>();
            events = new List<OFEventType>();
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
            foreach (OFEventType eventType in events) {
                eventDispatcher.On(eventType, EventHandler);
            }
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