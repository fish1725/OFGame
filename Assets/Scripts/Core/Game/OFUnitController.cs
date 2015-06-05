#region

using Assets.Scripts.Core.Event;

#endregion

namespace Assets.Scripts.Core.Game {
    public class OFUnitController {
        #region Methods

        [OFMethodCall]
        public static string GetUnitName(OFUnit unit) {
            return unit.name;
        }

        #endregion
    }
}