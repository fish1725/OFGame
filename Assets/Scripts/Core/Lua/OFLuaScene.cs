#region

using Assets.Scripts.Core.Game;

#endregion

namespace Assets.Scripts.Core.Lua {
    [CustomLuaClass]
    public class OFLuaScene {
        #region Methods

        public static OFUnit CreateUnit() {
            return OFGame.Scene.CreateUnit();
        }

        #endregion
    }
}