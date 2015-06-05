#region

using System.Collections.Generic;

#endregion

namespace Assets.Scripts.Core.Game {
    public class OFGamePlayer {
        #region Constructors

        public OFGamePlayer() {
            OwnUnits = new List<OFUnit>();
            ControlUnits = new List<OFUnit>();
        }

        #endregion

        #region Properties

        public List<OFUnit> ControlUnits { get; set; }
        public List<OFUnit> OwnUnits { get; set; }

        public void AddControlUnit(OFUnit unit) {
            ControlUnits.Add(unit);
        }

        #endregion
    }
}