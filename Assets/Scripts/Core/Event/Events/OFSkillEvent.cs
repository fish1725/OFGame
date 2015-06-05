#region

using Assets.Scripts.Core.Game;

#endregion

namespace Assets.Scripts.Core.Event.Events {
    public class OFSkillEvent : OFEvent {
        #region Properties

        public OFSkill Skill { get; set; }

        #endregion

        #region Methods

        public override void OnDestroy() {
            base.OnDestroy();
            Skill = null;
        }

        #endregion
    }
}