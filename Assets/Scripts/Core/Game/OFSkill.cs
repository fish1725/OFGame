#region

using System;
using Assets.Scripts.Core.Event;
using Assets.Scripts.Core.Event.Events;
using Assets.Scripts.Core.Utils.Pool;
using Assets.Scripts.Core.VO;

#endregion

namespace Assets.Scripts.Core.Game {
    public class OFSkill {
        #region Fields

        private readonly OFEventDispatcher _eventDispatcher;
        private readonly OFTimeline _timeline;
        private readonly OFUnit _unit;
        private readonly OFSkillVO _vo;

        #endregion

        #region Constructors

        public OFSkill(OFSkillVO vo, OFUnit unit) {
            _vo = vo;
            _unit = unit;
            CooldownTimer = 0;
            _timeline = new OFTimeline();
            _eventDispatcher = new OFEventDispatcher();
            Register();
        }

        #endregion

        #region Properties

        public string AnimName {
            get { return _vo.animName; }
        }

        public float CooldownTime {
            get { return _vo.cooldownTime; }
        }

        public float CooldownTimer { get; set; }

        public Guid ID {
            get { return _vo.id; }
        }

        public string Name {
            get { return _vo.name; }
        }

        #endregion

        #region Methods

        public void Register() {
            foreach (IOFTrigger trigger in _vo.triggers) {
                trigger.Register(_eventDispatcher);
                trigger.Register(_timeline);
            }
        }

        public void StartCast() {
            if (StartCast_Validation()) {
                StartCast_Implement();
            }
        }

        public void StartCast_Implement() {
            _unit.PlayAnimation(AnimName, 5, true);
            _timeline.duration = 5;
            _timeline.Start();
            CooldownTimer = CooldownTime;
            OFSkillEvent skillEvent = OFPool.Instance.Pop<OFSkillEvent>();
            skillEvent.Type = OFEventType.SkillStartCast;
            OFGame.Trigger(skillEvent);
            OFPool.Instance.Push(skillEvent);
        }

        public bool StartCast_Validation() {
            if (CooldownTimer > 0) {
                return false;
            }
            return true;
        }

        public void Update() {
            UpdateTimer();
        }

        private void UpdateTimer() {
            if (CooldownTimer > 0) {
                CooldownTimer -= OFGameTime.deltaTime;
            }
            _timeline.Update();
        }

        #endregion
    }
}