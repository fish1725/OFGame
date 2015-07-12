#region

using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Event;
using UnityEngine;
using System.Collections;

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
		[OFUIListItemCell(typeof(String))]
        public string animName { get; set; }
		[OFUIListItemCell(typeof(String))]
        public float cooldownTime { get; set; }
		[OFUIListItemCell(typeof(String))]
        public Guid id { get; private set; }
		[OFUIListItemCell(typeof(String))]
        public string name { get; set; }
		[OFUIListItemCell(typeof(Sprite))]
        public Sprite spriteName { get; set; }
		[OFUIListItemCell(typeof(List<object>))]
        public List<IOFTrigger> triggers { get; set; }

        #endregion
    }
}