#region

using System.Collections.Generic;
using System.Linq.Expressions;
using Assets.Scripts.Core.Asset;
using Assets.Scripts.Core.Event;
using Assets.Scripts.Core.VO;
using UnityEngine;

#endregion

namespace Assets.Scripts.Core.Game {
    public class OFScene : MonoBehaviour {
        #region Fields

        private readonly List<OFSkillVO> _skillVOs = new List<OFSkillVO>();

        #endregion

        #region Properties

        public OFCamera Camera { get; set; }

        #endregion

        #region Methods

        public void Client_CreateUnit() {
        }

        public OFCamera CreateCamera() {
            GameObject go = new GameObject("Camera");
            go.transform.SetParent(transform);
            Camera = go.AddComponent<OFCamera>();
            return Camera;
        }

        public OFGameObject CreateEffect(string effectName, OFGameObject attachTo, Vector3 offset) {
            GameObject go = Instantiate(OFAssetManager.Instance.Get(effectName).Object as GameObject);
            go.transform.SetParent(transform);
            go.transform.localPosition = offset;
            return go.AddComponent<OFGameObject>();
        }

        public void CreateLight() {
            GameObject go = new GameObject("Light");
            go.transform.SetParent(transform);
            go.AddComponent<Light>();
        }

        public OFMissile CreateMissile() {
            GameObject go = new GameObject("Missile");
            go.transform.SetParent(transform);
            return go.AddComponent<OFMissile>();
        }

        public OFSkillVO CreateSkillVO() {
            OFSkillVO vo = new OFSkillVO {
                name = "skill1",
                animName = "Skill1",
                cooldownTime = 5,
                spriteName = OFAssetManager.Instance.Get("Icon_Fireball").Object as Sprite
            };
            OFTimelineTrigger timelineTrigger = new OFTimelineTrigger();
            timelineTrigger.normalizedTime = 0.5f;
            OFAction action = new OFAction();
            action.expression = Expression.Call(Expression.Constant(this), typeof (OFScene).GetMethod("OnSkill"));
            timelineTrigger.actions.Add(action);
			timelineTrigger.actions.Add(action);
            timelineTrigger.Compile();
            vo.triggers.Add(timelineTrigger);
            _skillVOs.Add(vo);
            return vo;
        }

        public void CreateTerrian() {
            GameObject go = new GameObject("Terrian");
            go.transform.SetParent(transform);
            GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            plane.transform.SetParent(go.transform);
        }

        public OFUnit CreateUnit() {
            GameObject go = new GameObject("Unit");
            go.transform.SetParent(transform);
            return go.AddComponent<OFUnit>();
        }

        public List<OFSkillVO> GetAllSkillVOs() {
            return _skillVOs;
        }

        public void OnSkill() {
            OFMissile missile = OFGame.Scene.CreateMissile();
            //missile.position = GetBoneTransform(HumanBodyBones.LeftHand).position;
            OFGame.Scene.CreateEffect("CFX3_Fireball_A", missile, Vector3.zero);
            //missile.Direction = forward;
        }

        public void Start() {
            CreateCamera();
            CreateTerrian();
            CreateLight();
        }

        #endregion
    }
}