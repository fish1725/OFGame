#region

using Assets.Scripts.Core.Asset;
using Assets.Scripts.Core.Game;
using Assets.Scripts.Core.UI.List;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Assets.Scripts.Core.UI.Radio;
using Assets.Scripts.Core.UI.Window;

#endregion

namespace Assets.Scripts.Core.UI {
    public class OFUIManager {
        #region Fields

        public static string backgroundSpriteResourcePath = "UI/Skin/Background.psd";
        public static Color defaultSelectableColor = new Color(1f, 1f, 1f, 0.2f);
        public static string layerName = "UI";
        public static string rootName = "Canvas";
        public static string standardSpritePath = "UI/Skin/UISprite.psd";
        public Font TextFont = Resources.GetBuiltinResource<Font>("Arial.ttf");
        public Sprite background;
        private static readonly OFUIManager _instance = new OFUIManager();
        private Canvas _canvas;
        private GameObject _root;

        #endregion

        #region Properties

        public static OFUIManager Instance {
            get { return _instance; }
        }

        public Canvas Canvas {
            get { return _canvas; }
        }

        #endregion

        #region Methods

        public static GameObject CreateNewUI() {
            var root = GameObject.Find(rootName);
            if (root == null) {
                // Root for the UI
                root = new GameObject(rootName) {layer = LayerMask.NameToLayer(layerName)};
                Canvas canvas = root.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                root.AddComponent<CanvasScaler>();
                root.AddComponent<GraphicRaycaster>();

                // if there is no event system add one...
                CreateEventSystem(null);
            }

            return root;
        }


        public static GameObject CreateUIObject(string name, GameObject parent) {
            GameObject go = new GameObject(name);
            go.AddComponent<RectTransform>();
            go.SetParent(parent);
            return go;
        }


        public static void SetDefaultColorTransitionValues(Selectable slider) {
            ColorBlock colors = slider.colors;
            colors.highlightedColor = new Color(0.882f, 0.882f, 0.882f);
            colors.pressedColor = new Color(0.698f, 0.698f, 0.698f);
            colors.disabledColor = new Color(0.521f, 0.521f, 0.521f);
        }

        public OFUIList CreateList(string name) {
            GameObject go = CreateUIObject(name, _root);
            OFUIList list = go.AddComponent<OFUIList>();
            return list;
        }

		public OFUIRadio CreateRadio(string name) {
			GameObject go = CreateUIObject (name, _root);
			OFUIRadio radio = go.AddComponent<OFUIRadio> ();
			return radio;
		}

		public OFUIWindow CreateWindow(string title) {
			GameObject go = CreateUIObject(title, _root);
			OFUIWindow window = go.AddComponent<OFUIWindow>();
			window.title = title;
			return window;
		}

		public OFUIScrollRect CreateScrollRect() {
			GameObject go = CreateUIObject ("ScrollRect", _root);
			OFUIScrollRect scrollRect = go.AddComponent<OFUIScrollRect> ();
			return scrollRect;
		}

        public OFUIScrollBar CreateScrollBar(string name) {
            GameObject go = CreateUIObject(name, _root);
            OFUIScrollBar bar = go.AddComponent<OFUIScrollBar>();
            return bar;
        }

        public OFUIText CreateText(string name, string text) {
            GameObject go = CreateUIObject(name, _root);
            OFUIText t = go.AddComponent<OFUIText>();
            t.text = text;
            t.font = TextFont;
            return t;
        }

        public void Destroy() {
            if (_root != null) {
                Object.Destroy(_root.gameObject);
            }
        }

        public void Initialize() {
            if (_root == null) {
                _root = CreateNewUI();
                background = OFAssetManager.Instance.Get("ScrollBar_Background").Object as Sprite;
            }
        }

        private static void CreateEventSystem(GameObject parent) {
            var esys = Object.FindObjectOfType<EventSystem>();
            if (esys == null) {
                var eventSystem = new GameObject("EventSystem");
                eventSystem.SetParent(parent);
                eventSystem.AddComponent<EventSystem>();
                eventSystem.AddComponent<StandaloneInputModule>();
                eventSystem.AddComponent<TouchInputModule>();
            }
        }

        #endregion
    }
}