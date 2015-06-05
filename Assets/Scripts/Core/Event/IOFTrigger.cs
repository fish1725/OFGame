namespace Assets.Scripts.Core.Event {
    public interface IOFTrigger {
        #region Methods

        void Compile();
        void Register(IOFEventDispatcher eventDispatcher);

        #endregion
    }
}