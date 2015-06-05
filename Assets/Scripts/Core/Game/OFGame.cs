#region

using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using Assets.Scripts.Core.Asset;
using Assets.Scripts.Core.Event;
using Assets.Scripts.Core.Event.Events;
using Assets.Scripts.Core.Loader;
using Assets.Scripts.Core.UI;
using Assets.Scripts.Core.UI.List;
using Assets.Scripts.Core.Utils.Debug;
using UnityEngine;

#endregion

namespace Assets.Scripts.Core.Game {
    [CustomLuaClass]
    public class OFGame : MonoBehaviour {
        #region Fields

        private readonly OFEventDispatcher _eventDispatcher = new OFEventDispatcher();
        private static OFGame _instance;
        private OFAction _action;
        private OFCondition _condition;
        private OFUIList _list;

        private OFGamePlayer _localPlayer;
        private OFScene _scene;

        #endregion

        #region Properties

        public static OFGame Instance {
            get { return _instance; }
        }

        public static OFGamePlayer LocalPlayer {
            get { return _instance._localPlayer; }
        }

        public static OFScene Scene {
            get { return _instance._scene; }
        }

        #endregion

        #region Methods

        public static void Off(OFEventType eventType, OFEventHandler func) {
            _instance._eventDispatcher.Off(eventType, func);
        }

        public static void On(OFEventType eventType, OFEventHandler func) {
            _instance._eventDispatcher.On(eventType, func);
        }

        public static void StartGameCoroutine(IEnumerator routine) {
            _instance.StartCoroutine(routine);
        }

        public static string Test4(params object[] args) {
            return "123";
        }

        public static void Trigger(OFEvent e) {
            _instance._eventDispatcher.Trigger(e);
        }

        public static void Trigger(OFEventType eventType, OFEvent e = null) {
            _instance._eventDispatcher.Trigger(eventType, e);
        }

        public void Awake() {
            _instance = this;
        }


        public void Server_CreateScene() {
            GameObject go = new GameObject("Scene");
            _scene = go.AddComponent<OFScene>();
        }

        public void Start() {
            OFDebug debug = new OFDebug();
            debug.Init();
            OFGameTime.Start();

            On(OFEventType.LoadItemComplete, OnLoadItemComplete);

            OFAssetManager.Instance.Load();

            _localPlayer = new OFGamePlayer();
            Server_CreateScene();


            _condition = new OFCondition();
            _condition.expression = Expression.MakeBinary(ExpressionType.GreaterThan, Expression.Constant(1),
                Expression.Constant(0));
            _condition.Compile();
            _action = new OFAction();
            _action.expression =
                Expression.Call(Expression.Constant(this), typeof (OFGame).GetMethod("Test2"));
            _action.Compile();

            _scene.CreateSkillVO();
        }


        public void Test() {
            //OFUnit unit = _scene.CreateUnit();
            //LocalPlayer.ControlUnits.Add(unit);
            //LocalPlayer.OwnUnits.Add(unit);
            //_scene.Camera.target = unit;
            //foreach (OFUnit unit in LocalPlayer.ControlUnits) {
            //    unit.Freeze();
            //}
            _list = OFUIManager.Instance.CreateList("List");
            _list.width = 300;
            _list.scorllbarWidth = 20;
        }

        public void Test2() {
            //foreach (OFUnit unit in LocalPlayer.ControlUnits) {
            //    unit.StartSkill(unit.Skills.First().Value.id);
            //}
            _list.AddItem(_scene.CreateSkillVO());
        }

        public void Test3() {
            //LuaSvr svr = new LuaSvr();
            //svr.start("OFGame");
            _list.AddProperty("spriteName", getValue, setValue);
        }

        public void Update() {
            Trigger(OFEventType.GameUpdate);
            OFInputManager.Instance.Update();
        }

        private void OnLoadItemComplete(params object[] args) {
            OFLoadEvent loadEvent = args[0] as OFLoadEvent;
            if (loadEvent != null) {
                OFLoadItem item = loadEvent.Data;
                if (item.name == OFAssetType.Atlas.ToString()) {
                    OFUIManager.Instance.Initialize();
                }
            }
        }

        private object getValue(object model, string propertyName) {
            if (model != null) {
                PropertyInfo pi = model.GetType()
                .GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (pi != null) {
                    return pi.GetValue(model, null);
                }
            }
            return propertyName;
        }

        private void setValue(object model, string propertyName, object value) {
            if (model != null) {
                PropertyInfo pi = model.GetType()
                    .GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (pi != null) {
                    pi.SetValue(model, value, null);
                }
            }
        }

        #endregion
    }
}