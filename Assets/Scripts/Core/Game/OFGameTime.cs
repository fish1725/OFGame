#region

using Assets.Scripts.Core.Event;
using Assets.Scripts.Core.Utils.Timer;

#endregion

namespace Assets.Scripts.Core.Game {
    public static class OFGameTime {
        #region Fields

        private static readonly OFTimer _timer = new OFTimer();

        #endregion

        #region Properties

        public static float deltaTime {
            get { return _timer.DeltaTime; }
        }

        public static float now {
            get { return _timer.Elapsedtime; }
        }

        #endregion

        #region Methods

        public static void Start() {
            OFGame.On(OFEventType.GameUpdate, Update);
            _timer.Start();
        }

        public static void Update(params object[] args) {
            _timer.Update();
        }

        #endregion
    }
}