#region

using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Game;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Assets.Scripts.Core.UI.List {
    public class OFUIList : OFUIComponent {
        #region Fields

        private readonly List<OFUIListItem> _items = new List<OFUIListItem>();
        private OFUIListItem _banner;
        private LayoutElement _bannerLayoutElement;
        private float _bannerWidth = 15f;
        private RectTransform _content;
        private Scrollbar.Direction _direction = Scrollbar.Direction.BottomToTop;
        private LayoutElement _horizontalLayoutElement;
        private OFUIScrollBar _horizontalScrollBar;
        private LayoutGroup _layout1;
        private LayoutGroup _layout2;
        private GameObject _main;
        private ScrollRect _scrollRect;
        private LayoutElement _scrollRectLayoutElement;
        private float _scrollbarWidth = 15f;
        private LayoutElement _verticaLayoutElement;
        private OFUIScrollBar _verticalScrollbar;

        #endregion

        #region Properties

        public float bannerWidth {
            get { return _bannerWidth; }
            set {
                _bannerWidth = value;
                CalcBanner();
                CalcScrollRect();
            }
        }

        public Scrollbar.Direction direction {
            get { return _direction; }
            set {
                _direction = value;
                if (_layout1 != null) {
                    Destroy(_layout1);
                }
                if (_layout2 != null) {
                    Destroy(_layout2);
                }
                LayoutGroup layout = _content.GetComponent<LayoutGroup>();
                if (layout) {
                    Destroy(layout);
                }
                ContentSizeFitter contentSizeFitter = _content.gameObject.GetComponent<ContentSizeFitter>();
                switch (value) {
                    case Scrollbar.Direction.BottomToTop:
                    case Scrollbar.Direction.TopToBottom:
                        HorizontalLayoutGroup hlg = gameObject.AddComponent<HorizontalLayoutGroup>();
                        hlg.childForceExpandWidth = false;
                        _layout1 = hlg;
                        VerticalLayoutGroup vlg = _main.AddComponent<VerticalLayoutGroup>();
                        vlg.childForceExpandHeight = false;
                        _layout2 = vlg;
                        _content.gameObject.AddComponent<VerticalLayoutGroup>();
                        contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                        contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
                        break;
                    case Scrollbar.Direction.LeftToRight:
                    case Scrollbar.Direction.RightToLeft:
                        vlg = gameObject.AddComponent<VerticalLayoutGroup>();
                        vlg.childForceExpandHeight = false;
                        _layout1 = vlg;
                        hlg = _main.AddComponent<HorizontalLayoutGroup>();
                        hlg.childForceExpandWidth = false;
                        _layout2 = hlg;
                        _content.gameObject.AddComponent<HorizontalLayoutGroup>();
                        contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
                        contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                        break;
                }
                bannerWidth = _bannerWidth;
            }
        }

        public bool horizontalScrollbarVisible {
            get { return _horizontalScrollBar.gameObject.activeSelf; }
            set {
                _horizontalScrollBar.gameObject.SetActive(value);
                CalcScrollRect();
            }
        }

        public float scorllbarWidth {
            get { return _scrollbarWidth; }
            set {
                _scrollbarWidth = value;
                _verticalScrollbar.width = value;
                _horizontalScrollBar.height = value;
                CalcBanner();
                CalcScrollRect();
            }
        }

        public bool verticalScrollbarVisible {
            get { return _verticalScrollbar.gameObject.activeSelf; }
            set {
                _verticalScrollbar.gameObject.SetActive(value);
                CalcScrollRect();
            }
        }

        #endregion

        #region Methods

        public override void Awake() {
            base.Awake();
            Init();
        }

        public override void LateUpdate() {
            base.LateUpdate();
            for (int i = 0; i < _banner.cells.Count; i++) {
                foreach (OFUIListItem item in _items) {
                    switch (_direction) {
                        case Scrollbar.Direction.BottomToTop:
                        case Scrollbar.Direction.TopToBottom:
                            item.cells[i].layoutElement.preferredWidth = _banner.cells[i].layoutElement.preferredWidth;
                            break;
                        case Scrollbar.Direction.LeftToRight:
                        case Scrollbar.Direction.RightToLeft:
                            item.cells[i].layoutElement.preferredHeight = _banner.cells[i].layoutElement.preferredHeight;
                            break;
                    }
                }
            }
        }

        public void AddItem(object item) {
            GameObject go = OFUIManager.CreateUIObject("Item", _content.gameObject);
            OFUIListItem listItem = go.AddComponent<OFUIListItem>();
            listItem.model = item;
            listItem.direction = _direction;
            foreach (OFUIListItemCell cell in _banner.cells) {
                listItem.AddCell(cell.propertyName, cell.getValue, cell.setValue, cell.canWrite);
            }
            _items.Add(listItem);
        }

        public void AddProperty(string propertyName, Func<object, string, object> getValue,
            Action<object, string, object> setValue, Func<object, string, bool> canWrite) {
            _banner.AddCell(propertyName, getValue, setValue, canWrite);
            foreach (OFUIListItem item in _items) {
                item.AddCell(propertyName, getValue, setValue, canWrite);
            }
        }


        public void ClearItems() {
            _items.Clear();
            _content.gameObject.DestroyChildren();
        }

        private void CalcBanner() {
            switch (_direction) {
                case Scrollbar.Direction.BottomToTop:
                case Scrollbar.Direction.TopToBottom:
                    _bannerLayoutElement.preferredHeight = _bannerWidth;
                    _bannerLayoutElement.flexibleHeight = 0;
                    //_banner.rectTransform.offsetMin = new Vector2(0, -_bannerWidth);
                    //_banner.rectTransform.offsetMax = new Vector2(-_scrollbarWidth, 0);
                    break;
                case Scrollbar.Direction.LeftToRight:
                case Scrollbar.Direction.RightToLeft:
                    _bannerLayoutElement.preferredWidth = _bannerWidth;
                    _bannerLayoutElement.flexibleWidth = 0;
                    //_banner.rectTransform.offsetMin = new Vector2(_bannerWidth, _scrollbarWidth);
                    //_banner.rectTransform.offsetMax = Vector2.zero;
                    break;
            }
        }

        private void CalcScrollRect() {
            //RectTransform rt = _scrollRect.gameObject.GetComponent<RectTransform>();
            //rt.anchoredPosition = Vector3.zero;
            //Vector2 offsetMinPos = rt.offsetMin;
            //Vector2 offsetMaxPos = rt.offsetMax;
            switch (_direction) {
                case Scrollbar.Direction.BottomToTop:
                case Scrollbar.Direction.TopToBottom:
                    _scrollRectLayoutElement.flexibleHeight = 1;
                    //offsetMinPos.x = 0;
                    //offsetMaxPos.y = -_bannerWidth;
                    break;
                case Scrollbar.Direction.LeftToRight:
                case Scrollbar.Direction.RightToLeft:
                    _scrollRectLayoutElement.flexibleWidth = 1;
                    //offsetMinPos.x = _bannerWidth;
                    //offsetMaxPos.y = 0;
                    break;
            }
            //if (_verticalScrollbar.gameObject.activeSelf) {
            //    offsetMaxPos.x = -_scrollbarWidth;
            //} else {
            //    offsetMaxPos.x = 0;
            //}
            //if (_horizontalScrollBar.gameObject.activeSelf) {
            //    offsetMinPos.y = _scrollbarWidth;
            //} else {
            //    offsetMinPos.y = 0;
            //}
            //rt.offsetMin = offsetMinPos;
            //rt.offsetMax = offsetMaxPos;
        }

        private void Init() {
            _main = OFUIManager.CreateUIObject("Main", gameObject);

            GameObject go = OFUIManager.CreateUIObject("Banner", _main);
            _banner = go.AddComponent<OFUIListItem>();
            _bannerLayoutElement = _banner.gameObject.AddComponent<LayoutElement>();
            _banner.direction = _direction;
            _banner.rectTransform.anchorMin = new Vector2(0, 1);
            _banner.rectTransform.anchorMax = Vector2.one;

            GameObject scrollRectGo = OFUIManager.CreateUIObject("ScrollRect", _main);
            _scrollRect = scrollRectGo.AddComponent<ScrollRect>();
            _scrollRectLayoutElement = _scrollRect.gameObject.AddComponent<LayoutElement>();
            RectTransform rt = _scrollRect.gameObject.GetComponent<RectTransform>();
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;

            GameObject contentGo = OFUIManager.CreateUIObject("Content", _scrollRect.gameObject);
            _content = contentGo.GetComponent<RectTransform>();
            _content.gameObject.AddComponent<ContentSizeFitter>();
            rt = _content.gameObject.GetComponent<RectTransform>();
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;
            rt.pivot = new Vector2(0, 1.0f);
            _scrollRect.content = _content;


            _verticalScrollbar = OFUIManager.Instance.CreateScrollBar("Vertical ScrollBar");
            _verticalScrollbar.gameObject.SetParent(gameObject);
            _verticaLayoutElement = _verticalScrollbar.gameObject.AddComponent<LayoutElement>();
            _verticaLayoutElement.preferredWidth = _scrollbarWidth;
            _verticaLayoutElement.flexibleWidth = 0;
            _verticalScrollbar.direction = Scrollbar.Direction.BottomToTop;
            //rt = _verticalScrollbar.gameObject.GetComponent<RectTransform>();
            //rt.anchorMin = new Vector2(1, 0);
            //rt.anchorMax = Vector2.one;
            //rt.pivot = new Vector2(1, 0.5f);
            //rt.offsetMin = Vector2.zero;
            //rt.offsetMax = Vector2.zero;
            //_verticalScrollbar.width = _scrollbarWidth;

            _horizontalScrollBar = OFUIManager.Instance.CreateScrollBar("Horizontal ScrollBar");
            _horizontalScrollBar.gameObject.SetParent(gameObject);
            _horizontalLayoutElement = _horizontalScrollBar.gameObject.AddComponent<LayoutElement>();
            _horizontalLayoutElement.preferredHeight = _scrollbarWidth;
            _horizontalLayoutElement.flexibleHeight = 0;
            _horizontalScrollBar.direction = Scrollbar.Direction.RightToLeft;
            //rt = _horizontalScrollBar.gameObject.GetComponent<RectTransform>();
            //rt.anchorMin = Vector2.zero;
            //rt.anchorMax = new Vector2(1, 0);
            //rt.pivot = new Vector2(0.5f, 0.0f);
            //rt.offsetMin = Vector2.zero;
            //rt.offsetMax = Vector2.zero;
            //_horizontalScrollBar.height = _scrollbarWidth;

            _scrollRect.verticalScrollbar = _verticalScrollbar.gameObject.GetComponent<Scrollbar>();
            _scrollRect.horizontalScrollbar = _horizontalScrollBar.gameObject.GetComponent<Scrollbar>();


            Image bgImage = scrollRectGo.AddComponent<Image>();
            bgImage.sprite = OFUIManager.Instance.background;
            bgImage.type = Image.Type.Sliced;
            bgImage.color = new Color(0, 0, 0, 0);
            gameObject.AddComponent<Mask>();

            bgImage = gameObject.AddComponent<Image>();
            bgImage.sprite = OFUIManager.Instance.background;
            bgImage.type = Image.Type.Sliced;
            bgImage.color = OFUIManager.defaultSelectableColor;


            direction = Scrollbar.Direction.BottomToTop;
            verticalScrollbarVisible = true;
            horizontalScrollbarVisible = false;
            scorllbarWidth = 15f;
            bannerWidth = 20f;
        }

        public void Save() {
            
        }

        #endregion
    }
}