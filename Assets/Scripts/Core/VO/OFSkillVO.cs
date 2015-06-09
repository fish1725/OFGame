#region

using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Event;
using UnityEngine;

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

        public string animName { get; set; }

        public float cooldownTime { get; set; }

        public Guid id { get; private set; }

        public string name { get; set; }

        public Sprite spriteName { get; set; }

        public List<IOFTrigger> triggers { get; set; }

        #endregion
    }
}