#region

using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Assets.Scripts.Core.UI.List {
    public class OFUIListItemCellValueSprite : OFUIListItemCellValue {
        #region Fields

        private Image _image;

        #endregion

        #region Properties

        public override float preferredWidth {
            get { return _image.preferredWidth; }
        }

        public override object propertyValue {
            get { return _value; }
            set {
                _value = value;
                _image.sprite = value as Sprite;
            }
        }

        #endregion

        #region Methods

        public override void Awake() {
            base.Awake();
            _image = gameObject.AddComponent<Image>();
            _image.sprite = _value as Sprite;
            _image.rectTransform.anchorMin = Vector2.zero;
            _image.rectTransform.anchorMax = Vector2.one;
            _image.rectTransform.offsetMin = Vector2.zero;
            _image.rectTransform.offsetMax = Vector2.zero;
        }

        #endregion
    }
}