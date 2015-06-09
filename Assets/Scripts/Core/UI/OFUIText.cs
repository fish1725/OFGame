#region

using System;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Assets.Scripts.Core.UI {
    public class OFUIText : OFUIComponent {
        #region Fields

        private Text _text;

        #endregion

        #region Properties

        public Font font {
            get { return _text.font; }
            set { _text.font = value; }
        }

        public string propertyName { get; set; }
        public Type propertyType { get; set; }

        public object propertyValue {
            get { return _text.text; }
            set { _text.text = value.ToString(); }
        }

        public string text {
            get { return _text.text; }
            set { _text.text = value; }
        }

        #endregion

        #region Methods

        public override void Awake() {
            base.Awake();
            Init();
        }

        private void Init() {
            _text = gameObject.AddComponent<Text>();
            _text.font = OFUIManager.Instance.TextFont;
        }

        #endregion
    }
}