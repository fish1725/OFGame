using System;
using System.Reflection;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Assets.Scripts.Core.UI.List
{
	public class OFUIListItemCellValueList : OFUIListItemCellValue
	{
		private OFUIList _list;
		private HorizontalLayoutGroup _layout;
		public OFUIListItemCellValueList ()
		{
		}

		public override object propertyValue {
			get {
				return base.propertyValue;
			}
			set {
				base.propertyValue = value;
				foreach (object item in (_value as IList)) {
					_list.AddItem(item);
				}
				_list.AddProperty("value", (o, s) => {
					if (o == null) {
						return s;
					}
					return o;
				}, null, null, (o, s) => {
					if (o == null) {
						return typeof(String);
					}
					return cellType.GetGenericArguments()[0];
				});
			}
		}

		public override void Awake ()
		{
			base.Awake ();
			_layout = gameObject.AddComponent<HorizontalLayoutGroup> ();
			_layout.childForceExpandHeight = false;
			_layout.childForceExpandWidth = false;
			_list = OFUIManager.Instance.CreateList ("List");
			_list.gameObject.transform.SetParent (gameObject.transform);

			_list.rectTransform.anchorMin = new Vector2(0.0f, 0.0f);
			_list.rectTransform.anchorMax = new Vector2(0.1f, 0.1f);
			_list.rectTransform.offsetMin = Vector2.zero;
			_list.rectTransform.offsetMax = Vector2.zero;
			_list.verticalScrollbarVisible = false;
			_list.bannerWidth = 0;

		}
	}
}

