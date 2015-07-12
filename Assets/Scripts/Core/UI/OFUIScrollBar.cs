#region

using Assets.Scripts.Core.Asset;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Assets.Scripts.Core.UI {
    public class OFUIScrollBar : OFUIComponent {
        #region Fields

        private Scrollbar _scrollbar;

        #endregion

        #region Properties

        public Scrollbar.Direction direction {
            get { return _scrollbar.direction; }
            set { _scrollbar.direction = value; }
        }

        #endregion

        #region Methods

        public override void Awake() {
            base.Awake();
            Init();
        }

        private void Init() {
            GameObject sliderArea = OFUIManager.CreateUIObject("Sliding Area", gameObject);
            GameObject handle = OFUIManager.CreateUIObject("Handle", sliderArea);

            Image bgImage = gameObject.AddComponent<Image>();
            bgImage.sprite = OFAssetManager.Instance.Get("ScrollBar_Background").Object as Sprite;
            bgImage.type = Image.Type.Sliced;
            bgImage.color = OFUIManager.defaultSelectableColor;

            Image handleImage = handle.AddComponent<Image>();
            handleImage.sprite = OFAssetManager.Instance.Get("ScrollBar_Thumb_Normal").Object as Sprite;
            handleImage.type = Image.Type.Sliced;
			handleImage.color = new Color (1f, 1f, 1f, 0.75f);

            RectTransform sliderAreaRect = sliderArea.GetComponent<RectTransform>();
            sliderAreaRect.sizeDelta = new Vector2(-20, -20);
            sliderAreaRect.anchorMin = Vector2.zero;
            sliderAreaRect.anchorMax = Vector2.one;

            RectTransform handleRect = handle.GetComponent<RectTransform>();
            handleRect.sizeDelta = new Vector2(20, 20);

            _scrollbar = gameObject.AddComponent<Scrollbar>();
            _scrollbar.handleRect = handleRect;
            _scrollbar.targetGraphic = handleImage;
            OFUIManager.SetDefaultColorTransitionValues(_scrollbar);
        }

        #endregion
    }
}