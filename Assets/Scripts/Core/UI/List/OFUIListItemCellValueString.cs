#region

using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Assets.Scripts.Core.UI.List {
    public class OFUIListItemCellValueString : OFUIListItemCellValue {
        #region Fields

        private LayoutElement _layoutElement;
        private OFUIInputField _inputField;

        #endregion

        #region Properties

        public override bool canWrite {
            get { return _inputField.interactable; }
            set { _inputField.interactable = value; }
        }

        public override float preferredWidth {
            get { return _inputField.preferredWidth; }
        }

        public override object propertyValue {
            get { return _value; }
            set {
                _value = value;
                _inputField.text = value.ToString();
            }
        }

        #endregion

        #region Methods

        public override void Awake() {
            base.Awake();
            _inputField = gameObject.AddComponent<OFUIInputField>();
            _inputField.font = OFUIManager.Instance.TextFont;
            _inputField.rectTransform.anchorMin = Vector2.zero;
            _inputField.rectTransform.anchorMax = Vector2.one;
            _inputField.rectTransform.offsetMin = Vector2.zero;
            _inputField.rectTransform.offsetMax = Vector2.zero;


            _layoutElement = gameObject.AddComponent<LayoutElement>();
            _layoutElement.flexibleWidth = 1;
            _layoutElement.flexibleHeight = 1;
        }

        #endregion
    }
}