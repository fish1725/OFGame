#region

using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Event.Events;
using Assets.Scripts.Core.Utils.Pool;
using Assets.Scripts.Core.Utils.Timer;

#endregion

namespace Assets.Scripts.Core.Event {
    public class OFTimeline : IOFEventDispatcher {
        #region Fields

        private readonly SortedDictionary<float, OFEventHandler> _events;
        private readonly OFTimer _timer;
        private float _currentEventNormalizedTime;

        #endregion

        #region Constructors

        public OFTimeline() {
            _timer = new OFTimer();
            _events = new SortedDictionary<float, OFEventHandler>();
        }

        #endregion

        #region Properties

        public float duration {
            get { return _timer.Interval; }
            set { _timer.Interval = value; }
        }

        #endregion

        #region Methods

        public void On(OFEventType eventType, OFEventHandler func) {
            
        }

        public void On(float normalizedTime, OFEventHandler func) {
            if (_events.ContainsKey(normalizedTime)) {
                _events[normalizedTime] += func;
            } else {
                _events[normalizedTime] = func;
            }
        }

        public void Start() {
            _timer.Start();
            _currentEventNormalizedTime = float.MinValue;
        }

        public void Update() {
            _timer.Update();
            if (_timer.IsTicking) {
                float maxKey = _currentEventNormalizedTime;
                foreach (float key in _events.Keys) {
                    if (key <= _timer.NormalizedTime && key > _currentEventNormalizedTime) {
                        OFTimelineEvent timelineEvent = OFPool.Instance.Pop<OFTimelineEvent>();
                        _events[key](timelineEvent);
                        OFPool.Instance.Push(timelineEvent);
                        if (maxKey < key) {
                            maxKey = key;
                        }
                    }
                }
                _currentEventNormalizedTime = maxKey;
            }
        }

        #endregion
    }
}