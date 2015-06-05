#region

using Assets.Scripts.Core.Game;
using UnityEngine;

#endregion

namespace Assets.Scripts.Core.UI {
    public class OFUIComponent : OFGameObject {
        #region Fields

        private RectTransform _rectTransform;

        #endregion

        #region Properties

        public float height {
            get { return _rectTransform.sizeDelta.y; }
            set {
                Vector2 size = _rectTransform.sizeDelta;
                size.y = value;
                _rectTransform.sizeDelta = size;
            }
        }

        public RectTransform rectTransform {
            get { return _rectTransform; }
            set { _rectTransform = value; }
        }

        public float width {
            get { return _rectTransform.sizeDelta.x; }
            set {
                Vector2 size = _rectTransform.sizeDelta;
                size.x = value;
                _rectTransform.sizeDelta = size;
            }
        }

        #endregion

        #region Methods

        public override void Awake() {
            base.Awake();
            _rectTransform = gameObject.GetComponent<RectTransform>();
        }

        #endregion
    }
}