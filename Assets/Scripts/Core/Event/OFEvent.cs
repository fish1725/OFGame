#region

using Assets.Scripts.Core.Utils.Pool;

#endregion

namespace Assets.Scripts.Core.Event {
    public class OFEvent : IOFPoolItem {
        #region Constructors

        public OFEvent() {
            Type = OFEventType.None;
        }

        #endregion

        #region Properties

        public OFEventType Type { get; set; }

        #endregion

        #region Methods

        public virtual void OnCreate() {
        }

        public virtual void OnDestroy() {
        }

        #endregion
    }
}