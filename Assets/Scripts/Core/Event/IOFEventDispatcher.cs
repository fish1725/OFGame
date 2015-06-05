namespace Assets.Scripts.Core.Event {
    public interface IOFEventDispatcher {
        #region Methods

        void On(OFEventType eventType, OFEventHandler func);
        void On(float normalizedTime, OFEventHandler func);

        #endregion
    }
}