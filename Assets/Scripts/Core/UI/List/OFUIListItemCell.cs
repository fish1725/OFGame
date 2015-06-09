#region

using System;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Assets.Scripts.Core.UI.List {
    public class OFUIListItemCell : OFUIComponent {
        #region Fields

        private Image _backgroundImage;

        private HorizontalLayoutGroup _layout;
        private LayoutElement _layoutElement;

        private object _model;
        private string _propertyName;
        private GameObject _valueGO;

        #endregion

        #region Properties

        public Sprite background {
            get { return _backgroundImage.sprite; }
            set { _backgroundImage.sprite = value; }
        }

        public Func<object, string, bool> canWrite { get; set; }
        public Func<object, string, object> getValue { get; set; }

        public LayoutElement layoutElement {
            get { return _layoutElement; }
            set { _layoutElement = value; }
        }

        public object model {
            get { return _model; }
            set {
                _model = value;
                if (_propertyName != null) {
                    Refresh();
                }
            }
        }

        public string propertyName {
            get { return _propertyName; }
            set {
                _propertyName = value;
                Refresh();
            }
        }

        public Action<object, string, object> setValue { get; set; }

        #endregion

        #region Methods

        public override void Awake() {
            base.Awake();
            _backgroundImage = gameObject.AddComponent<Image>();
            _backgroundImage.sprite = OFUIManager.Instance.background;
            _backgroundImage.type = Image.Type.Sliced;
            _backgroundImage.color = OFUIManager.defaultSelectableColor;
            _layoutElement = gameObject.AddComponent<LayoutElement>();
            _layout = gameObject.AddComponent<HorizontalLayoutGroup>();
            _layout.childForceExpandHeight = false;
            _layout.childForceExpandWidth = false;
            _layout.childAlignment = TextAnchor.MiddleCenter;
        }

        private void Refresh() {
            if (_valueGO == null) {
                _valueGO = OFUIManager.CreateUIObject("Value", gameObject);
            }
            object propertyValue;
            if (getValue != null) {
                propertyValue = getValue(model, _propertyName);
            } else {
                propertyValue = _propertyName;
            }
            Type baseType = typeof (OFUIListItemCellValue);
            Debug.Log("type: " + baseType.FullName + propertyValue.GetType().Name);
            Type propertyType = Type.GetType(baseType.FullName + propertyValue.GetType().Name);
            if (propertyType == null) {
                propertyType = baseType;
            }
            OFUIListItemCellValue cellValue = _valueGO.GetComponent(propertyType) as OFUIListItemCellValue;
            if (cellValue == null) {
                cellValue = _valueGO.AddComponent(propertyType) as OFUIListItemCellValue;
            }
            Debug.Assert(cellValue != null);
            if (cellValue != null) {
                if (canWrite != null) {
                    cellValue.canWrite = canWrite(model, _propertyName);
                } else {
                    cellValue.canWrite = false;
                }
                cellValue.propertyValue = propertyValue;
                _layoutElement.preferredWidth = cellValue.preferredWidth;
            }
        }

        #endregion
    }
}