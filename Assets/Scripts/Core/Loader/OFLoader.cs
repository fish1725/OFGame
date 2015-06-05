#region

using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Core.Event;
using Assets.Scripts.Core.Event.Events;
using Assets.Scripts.Core.Game;
using Assets.Scripts.Core.Utils.Pool;
using UnityEngine;

#endregion

namespace Assets.Scripts.Core.Loader {
    public class OFLoader {
        #region Fields

        private readonly OFEventDispatcher _eventDispatcher;
        private readonly Dictionary<string, OFLoadItem> _items;

        #endregion

        #region Constructors

        public OFLoader() {
            _items = new Dictionary<string, OFLoadItem>();
            _eventDispatcher = new OFEventDispatcher();
        }

        #endregion

        #region Properties

        public string Name { get; set; }

        #endregion

        #region Methods

        public void Add(string name, string url) {
            if (_items.ContainsKey(url)) {
                throw new Exception("Loader " + Name + " has dunplicate items " + url);
            }
            OFLoadItem item = new OFLoadItem {name = name, url = url};
            _items.Add(url, item);
        }

        public void Off(OFEventType eventType, OFEventHandler func) {
            _eventDispatcher.Off(eventType, func);
        }

        public void On(OFEventType eventType, OFEventHandler func) {
            _eventDispatcher.On(eventType, func);
        }

        public void Remove(string url) {
        }

        public void Start() {
            foreach (OFLoadItem item in _items.Values) {
                OFGame.StartGameCoroutine(StartItem(item));
            }
        }

        public IEnumerator StartItem(OFLoadItem item) {
            item.www = new WWW(item.url);
            yield return item.www;
            OFLoadEvent loadEvent = OFPool.Instance.Pop<OFLoadEvent>();
            loadEvent.Type = OFEventType.LoadItemComplete;
            loadEvent.Data = item;
            _eventDispatcher.Trigger(loadEvent);
            OFPool.Instance.Push(loadEvent);
        }

        #endregion
    }
}