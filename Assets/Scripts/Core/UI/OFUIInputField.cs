#region

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#endregion

namespace Assets.Scripts.Core.UI {
    public class OFUIInputField : OFUIComponent, IDragHandler, IBeginDragHandler, IEndDragHandler {
        #region Fields

        private Image _background;
        private InputField _inputField;
        private Text _placeHolder;
        private Text _text;

        #endregion

        #region Properties

        public Font font {
            get { return _text.font; }
            set {
                _text.font = value;
                _placeHolder.font = value;
            }
        }

        public bool interactable {
            get { return _inputField.interactable; }
            set { _inputField.interactable = value; }
        }

        public float preferredWidth {
            get { return _text.preferredWidth; }
        }

        public string text {
            get { return _inputField.text; }
            set { _inputField.text = value; }
        }

        #endregion

        #region Methods

        public override void Awake() {
            base.Awake();
            //_background = gameObject.AddComponent<Image>();
            //_background.sprite = OFUIManager.Instance.background;
            //_background.type = Image.Type.Sliced;

            GameObject go = OFUIManager.CreateUIObject("PlaceHolder", gameObject);
            _placeHolder = go.AddComponent<Text>();
            _placeHolder.font = OFUIManager.Instance.TextFont;
            _placeHolder.rectTransform.anchorMax = Vector2.one;
            _placeHolder.rectTransform.anchorMin = Vector2.zero;
            _placeHolder.rectTransform.offsetMax = Vector2.zero;
            _placeHolder.rectTransform.offsetMin = Vector2.zero;

            go = OFUIManager.CreateUIObject("Text", gameObject);
            _text = gameObject.AddComponent<Text>();
            _text.font = OFUIManager.Instance.TextFont;
            _text.rectTransform.anchorMax = Vector2.one;
            _text.rectTransform.anchorMin = Vector2.zero;
            _text.rectTransform.offsetMax = Vector2.zero;
            _text.rectTransform.offsetMin = Vector2.zero;
            _text.alignment = TextAnchor.MiddleCenter;

            _inputField = gameObject.AddComponent<InputField>();
            _inputField.targetGraphic = _text;
            _inputField.placeholder = _placeHolder;
            _inputField.textComponent = _text;
        }

        public void OnBeginDrag(PointerEventData eventData) {
            if (!interactable) {
                IBeginDragHandler dragHandler = gameObject.transform.parent.GetComponentInParent<IBeginDragHandler>();
                dragHandler.OnBeginDrag(eventData);
            }
        }

        public void OnDrag(PointerEventData eventData) {
            if (!interactable) {
                IDragHandler dragHandler = gameObject.transform.parent.GetComponentInParent<IDragHandler>();
                dragHandler.OnDrag(eventData);
            }
        }

        public void OnEndDrag(PointerEventData eventData) {
            if (!interactable) {
                IEndDragHandler dragHandler = gameObject.transform.parent.GetComponentInParent<IEndDragHandler>();
                dragHandler.OnEndDrag(eventData);
            }
        }

        public void Select() {
            _inputField.Select();
        }

        #endregion
    }
}