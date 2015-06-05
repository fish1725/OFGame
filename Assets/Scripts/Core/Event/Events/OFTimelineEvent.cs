namespace Assets.Scripts.Core.Event.Events {
    public class OFTimelineEvent : OFEvent {
        #region Fields

        public float normalizedTime;

        #endregion

        #region Constructors

        public OFTimelineEvent() {
            Type = OFEventType.Timeline;
        }

        #endregion
    }
}