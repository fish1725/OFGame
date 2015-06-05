#region

using System;
using System.Collections.Generic;

#endregion

namespace Assets.Scripts.Core.Utils.Pool {
    public class OFPool {
        #region Fields

        private static readonly OFPool _pool = new OFPool();
        private readonly Dictionary<object, Queue<object>> _poolDictionary;

        #endregion

        #region Constructors

        public OFPool() {
            _poolDictionary = new Dictionary<object, Queue<object>>();
        }

        #endregion

        #region Properties

        public static OFPool Instance {
            get { return _pool; }
        }

        #endregion

        #region Methods

        public T Pop<T>() where T : class, IOFPoolItem, new() {
            Type type = typeof (T);
            T t;
            if (!_poolDictionary.ContainsKey(type)) {
                _poolDictionary[type] = new Queue<object>();
            }
            if (_poolDictionary[type].Count == 0) {
                t = new T();
            } else {
                t = (T) (_poolDictionary[type].Dequeue());
            }
            return t;
        }

        public void Push<T>(T t) where T : class, IOFPoolItem {
            if (t != null) {
                Type type = typeof (T);
                if (!_poolDictionary.ContainsKey(type)) {
                    _poolDictionary[type] = new Queue<object>();
                }
                t.OnDestroy();
                _poolDictionary[type].Enqueue(t);
            }
        }

        #endregion
    }
}