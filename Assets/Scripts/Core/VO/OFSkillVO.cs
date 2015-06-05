#region

using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Event;
using Assets.Scripts.Core.UI;
using Assets.Scripts.Core.UI.List;

#endregion

namespace Assets.Scripts.Core.VO {
    public class OFSkillVO {
        #region Constructors

        public OFSkillVO() {
            id = Guid.NewGuid();
            triggers = new List<IOFTrigger>();
        }

        #endregion

        #region Properties

        [OFUIProperty("animName", typeof (OFUIText))]
        public string animName { get; set; }

        public float cooldownTime { get; set; }
        public Guid id { get; set; }

        [OFUIProperty("name", typeof (OFUIText))]
        public string name { get; set; }

        [OFUIProperty("spriteName", typeof (OFUIImage))]
        public string spriteName { get; set; }

        public List<IOFTrigger> triggers { get; set; }

        #endregion
    }
}