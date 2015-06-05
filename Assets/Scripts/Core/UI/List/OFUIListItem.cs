#region

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Assets.Scripts.Core.UI.List {
    public class OFUIListItem : OFUIComponent {
        #region Fields

        private readonly List<OFUIListItemCell> _cells = new List<OFUIListItemCell>();
        private Image _backgroundImage;
        private Button _button;
        private Scrollbar.Direction _direction = Scrollbar.Direction.BottomToTop;
        private LayoutGroup _layout;

        #endregion

        #region Properties

        public Sprite background {
            get { return _backgroundImage.sprite; }
            set { _backgroundImage.sprite = value; }
        }

        public List<OFUIListItemCell> cells {
            get { return _cells; }
        }

        public Scrollbar.Direction direction {
            get { return _direction; }
            set {
                _direction = value;
                if (_layout) {
                    Destroy(_layout);
                }
                switch (value) {
                    case Scrollbar.Direction.BottomToTop:
                    case Scrollbar.Direction.TopToBottom:
                        HorizontalLayoutGroup horizontalLayout = gameObject.AddComponent<HorizontalLayoutGroup>();
                        //horizontalLayout.childForceExpandWidth = false;
                        _layout = horizontalLayout;
                        break;
                    case Scrollbar.Direction.LeftToRight:
                    case Scrollbar.Direction.RightToLeft:
                        VerticalLayoutGroup verticalLayout = gameObject.AddComponent<VerticalLayoutGroup>();
                        //verticalLayout.childForceExpandHeight = false;
                        _layout = verticalLayout;
                        break;
                }
            }
        }

        public object model { get; set; }

        #endregion

        #region Methods

        public override void Awake() {
            base.Awake();
            _backgroundImage = gameObject.AddComponent<Image>();
            _backgroundImage.sprite = OFUIManager.Instance.background;
            _backgroundImage.type = Image.Type.Sliced;
            _backgroundImage.color = OFUIManager.defaultSelectableColor;

            _button = gameObject.AddComponent<Button>();
            Navigation customNav = new Navigation {mode = Navigation.Mode.None};
            _button.navigation = customNav;
            Color color = _backgroundImage.color;
            ColorBlock colorBlock = _button.colors;
            colorBlock.normalColor = color;
            color.a = 0.5f;
            colorBlock.highlightedColor = color;
            color.a = 0.8f;
            colorBlock.pressedColor = color;
            _button.colors = colorBlock;
        }

        public OFUIListItemCell AddCell(string propertyName, Func<object, string, object> getValue, Action<object, string, object> setValue) {
            GameObject go = OFUIManager.CreateUIObject(propertyName, gameObject);
            OFUIListItemCell cell = go.AddComponent<OFUIListItemCell>();
            cell.getValue = getValue;
            cell.setValue = setValue;
            cell.model = model;
            cell.propertyName = propertyName;
            cells.Add(cell);
            return cell;
        }

        #endregion
    }
}