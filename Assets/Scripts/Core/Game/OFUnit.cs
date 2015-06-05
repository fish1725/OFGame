#region

using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Asset;
using UnityEngine;

#endregion

namespace Assets.Scripts.Core.Game {
    [CustomLuaClass]
    public class OFUnit : MonoBehaviour {
        #region Fields

        public string animatorControllerName;
        public string[] attachPoints;
        public string modelName;
        public Vector3 motion;
        public float moveSpeed;
        public float rotateSpeed;
        public float upBodyRotationX = 0f;
        public float upBodyRotationY = 0f;
        private readonly Dictionary<Guid, OFSkill> _skills = new Dictionary<Guid, OFSkill>();
        private readonly Dictionary<int, OFState> _states = new Dictionary<int, OFState>();
        private Animator _animator;
        private Transform _spine;

        #endregion

        #region Properties

        public Dictionary<Guid, OFSkill> Skills {
            get { return _skills; }
        }

        #endregion

        #region Methods

        public void Awake() {
            InitConfig();
            InitModel();
            InitAnimator();
            InitSkills();
        }

        public void Freeze() {
            if (_animator != null) {
                _animator.speed = 0;
            }
        }

        public Transform GetBoneTransform(HumanBodyBones humanBoneId) {
            if (_animator != null) {
                return _animator.GetBoneTransform(humanBoneId);
            }
            return transform;
        }

        public void LateUpdate() {
            UpdateMotion();
        }

        [Lua]
        public void PlayAnimation(string animName, float time, bool loop) {
            if (_animator != null) {
                int hash = Animator.StringToHash(animName);
                OFState state;
                _states.TryGetValue(hash, out state);
                if (state != null) {
                    state.animTime = time;
                    state.duration = 4;
                    state.nextState = _states[Animator.StringToHash("Idle")];
                }
                _animator.CrossFade(animName, 0.1f, 0, 0.0f);
            }
        }

        public void StartSkill(Guid id) {
            OFSkill skill;
            Skills.TryGetValue(id, out skill);
            if (skill != null) {
                skill.StartCast();
            } else {
                Debug.LogError("Unit " + name + " has no skill " + id);
            }
        }

        public void Update() {
            UpdateSkills();
            UpdateAnimator();
        }

        private void InitAnimator() {
            _animator = gameObject.GetComponentInChildren<Animator>();
            if (_animator != null) {
                if (_animator.runtimeAnimatorController == null) {
                    _animator.runtimeAnimatorController =
                        OFAssetManager.Instance.Get(animatorControllerName).Object as RuntimeAnimatorController;
                }
                OFState[] states = _animator.GetBehaviours<OFState>();
                foreach (OFState state in states) {
                    _states[Animator.StringToHash(state.stateName)] = state;
                }
                _spine = _animator.GetBoneTransform(HumanBodyBones.Spine);
                //if (_animator.isOptimizable) {
                //    AnimatorUtility.DeoptimizeTransformHierarchy(_animator.gameObject);
                //    AnimatorUtility.OptimizeTransformHierarchy(_animator.gameObject,
                //        attachPoints);
                //}
            }
        }

        private void InitConfig() {
            modelName = "skeleton_king_yellow";
            animatorControllerName = "OFAnimatorController";
            moveSpeed = 1;
            rotateSpeed = 90;
            attachPoints = new[] {"Character1_LeftHand", "Character1_RightHand"};
        }

        private void InitModel() {
            GameObject go = Instantiate(OFAssetManager.Instance.Get(modelName).Object as GameObject);
            go.transform.SetParent(transform);
            go.transform.localPosition = Vector3.zero;
        }

        private void InitSkills() {
            //OFSkillVO vo = new OFSkillVO {name = "skill1", animName = "Skill1", cooldownTime = 5};
            //OFTimelineTrigger timelineTrigger = new OFTimelineTrigger();
            //timelineTrigger.normalizedTime = 0.5f;
            //OFAction action = new OFAction();
            //action.expression = Expression.Call(Expression.Constant(this), typeof (OFUnit).GetMethod("OnSkill"));
            //timelineTrigger.actions.Add(action);
            //timelineTrigger.Compile();
            //vo.triggers.Add(timelineTrigger);

            //OFSkill skill = new OFSkill(vo, this);
            //Skills.Add(skill.id, skill);
        }

        private void UpdateAnimator() {
            if (_animator != null) {
                //AnimatorStateInfo asi = _animator.GetCurrentAnimatorStateInfo(0);
                //float time;
                //_animTime.TryGetValue(asi.shortNameHash, out time);
                //if (time > 0) {
                //    _animator.speed = asi.length / time;
                //} else {
                //    _animator.speed = 1;
                //}
                //bool loop = true;
                //_animLoop.TryGetValue(asi.shortNameHash, out loop);
                //_animator.GetCurrentAnimatorClipInfo(0)[0].clip.wrapMode = WrapMode.PingPong;
            }
        }

        private void UpdateMotion() {
            if (motion.sqrMagnitude > 0) {
                Vector3 pos = transform.position;
                motion = transform.forward*motion.z + transform.right*motion.x;
                pos += moveSpeed*motion.normalized*OFGameTime.deltaTime;
                transform.position = pos;

                Quaternion rot = transform.rotation;
                rot = Quaternion.RotateTowards(rot, Quaternion.LookRotation(motion), rotateSpeed*OFGameTime.deltaTime);
                transform.rotation = rot;

                motion = Vector3.zero;
            }
            if (_spine != null) {
                _spine.Rotate(transform.up, upBodyRotationY, Space.World);
                _spine.Rotate(transform.right, upBodyRotationX, Space.World);
            }
        }

        private void UpdateSkills() {
            foreach (OFSkill skill in Skills.Values) {
                skill.Update();
            }
        }

        #endregion
    }
}