#region

using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Assets.Scripts.Core.UI.List {
    public class OFUIListItemCellValue : OFUIComponent {
        #region Fields

        private Text _text;
        private object _value;

        #endregion

        #region Properties

        public virtual float preferredWidth {
            get { return _text.preferredWidth; }
        }

        public virtual object propertyValue {
            get { return _value; }
            set {
                _value = value;
                _text.text = value.ToString();
            }
        }

        public object model { get; set; }
        public string propertyName { get; set; }

        #endregion

        #region Methods

        public override void Awake() {
            base.Awake();
            _text = gameObject.AddComponent<Text>();
            _text.font = OFUIManager.Instance.TextFont;
            _text.rectTransform.anchorMin = Vector2.zero;
            _text.rectTransform.anchorMax = Vector2.one;
            _text.rectTransform.offsetMin = Vector2.zero;
            _text.rectTransform.offsetMax = Vector2.zero;
        }

        #endregion
    }
}