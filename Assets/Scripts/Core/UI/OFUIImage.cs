#region

using System;
using Assets.Scripts.Core.Asset;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Assets.Scripts.Core.UI {
    public class OFUIImage : OFUIComponent {
        #region Fields

        private Image _image;

        #endregion

        #region Properties

        public string propertyName { get; set; }
        public Type propertyType { get; set; }

        public object propertyValue {
            get {
                if (propertyType == typeof (Sprite)) {
                    return _image.sprite;
                }
                return _image.sprite.name;
            }
            set {
                if (propertyType == typeof (Sprite)) {
                    _image.sprite = value as Sprite;
                } else {
                    _image.sprite = OFAssetManager.Instance.Get(value.ToString()).Object as Sprite;
                }
            }
        }

        public Sprite sprite {
            get { return _image.sprite; }
            set { _image.sprite = value; }
        }

        #endregion

        #region Methods

        public override void Awake() {
            base.Awake();
            _image = gameObject.AddComponent<Image>();
        }

        #endregion
    }
}