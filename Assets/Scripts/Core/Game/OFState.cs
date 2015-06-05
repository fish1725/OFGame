#region

using UnityEngine;

#endregion

namespace Assets.Scripts.Core.Game {
    public class OFState : StateMachineBehaviour {
        #region Fields

        public float animTime;
        public float duration;
        public OFState nextState;
        public string stateName;

        #endregion

        #region Methods

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            //OFDebug.Log("OnStateEnter " + animTime);
            if (animTime > 0) {
                animator.speed = stateInfo.length/animTime;
            } else {
                animator.speed = 1;
            }
        }

        public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash) {
            base.OnStateMachineEnter(animator, stateMachinePathHash);
            Debug.Log("OnStateMachineEnter " + stateMachinePathHash);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            if (duration > 0) {
                duration -= OFGameTime.deltaTime;
                if (duration <= 0) {
                    if (nextState != null) {
                        TransitionTo(animator, nextState.stateName, layerIndex);
                    }
                }
            }
            //OFDebug.Log("OnStateUpdate " + stateInfo.shortNameHash);
        }

        protected void TransitionTo(Animator animator, string state, int layerIndex) {
            animator.CrossFade(state, 0.15f, layerIndex, 0f);
        }

        #endregion
    }
}