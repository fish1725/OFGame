#region

using Assets.Scripts.Core.Loader;

#endregion

namespace Assets.Scripts.Core.Event.Events {
    public class OFLoadEvent : OFEvent {
        #region Fields

        public OFLoadItem Data;

        #endregion

        #region Constructors

        public OFLoadEvent() {
            Type = OFEventType.LoadItemStart;
        }

        #endregion

        #region Methods

        public override void OnDestroy() {
            base.OnDestroy();
            Data = null;
        }

        #endregion
    }
}