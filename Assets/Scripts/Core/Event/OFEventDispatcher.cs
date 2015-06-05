#region

using System;
using System.Collections.Generic;

#endregion

namespace Assets.Scripts.Core.Event {
    public delegate void OFEventHandler(params object[] args);

    public class OFEventDispatcher : IOFEventDispatcher {
        #region Fields

        private readonly Dictionary<OFEventType, OFEventHandler> _eventDictionary;

        #endregion

        #region Constructors

        public OFEventDispatcher() {
            _eventDictionary = new Dictionary<OFEventType, OFEventHandler>();
        }

        #endregion

        #region Methods

        public void Off(OFEventType eventType, OFEventHandler func) {
            if (_eventDictionary != null) {
                if (func != null) {
                    _eventDictionary[eventType] -= func;
                }
            }
        }

        public void On(OFEventType eventType, OFEventHandler func) {
            if (_eventDictionary.ContainsKey(eventType)) {
                _eventDictionary[eventType] += func;
            } else {
                _eventDictionary[eventType] = func;
            }
        }

        public void On(float normalizedTime, OFEventHandler func) {
            
        }

        public void Trigger(OFEventType eventType, OFEvent e = null) {
            if (_eventDictionary.ContainsKey(eventType)) {
                _eventDictionary[eventType](e, 123);
            }
        }

        public void Trigger(OFEvent e) {
            Trigger(e.Type, e);
        }

        #endregion
    }
}