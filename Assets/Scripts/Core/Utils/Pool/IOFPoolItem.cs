namespace Assets.Scripts.Core.Utils.Pool {
    public interface IOFPoolItem {
        #region Methods

        void OnCreate();
        void OnDestroy();

        #endregion
    }
}