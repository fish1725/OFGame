#region

using System;
using UnityEngine;

#endregion

namespace Assets.Scripts.Core.Utils.Timer {
    public class OFTimer {
        #region Fields

        private float _duration;
        private float _lastTime;

        #endregion

        #region Constructors

        public OFTimer() {
            Id = new Guid();
            Name = "Timer" + Id;
            AutoReset = false;

            Elapsedtime = 0;
            TimeScale = 1;
            IsTicking = false;
            Interval = float.MaxValue;
        }

        #endregion

        #region Properties

        public bool AutoReset { get; set; }
        public float DeltaTime { get; set; }
        public float Elapsedtime { get; set; }
        public Guid Id { get; set; }
        public float Interval { get; set; }
        public bool IsTicking { get; set; }
        public string Name { get; set; }
        public float NormalizedTime { get; set; }
        public float StartTime { get; set; }
        public float TimeScale { get; set; }

        #endregion

        #region Methods

        public void Pause() {
            IsTicking = false;
            DeltaTime = 0;
        }

        public void Resume() {
            IsTicking = true;
            _lastTime = Time.realtimeSinceStartup;
        }

        public void Start() {
            StartTime = Time.realtimeSinceStartup;
            _lastTime = StartTime;
            Elapsedtime = 0;
            IsTicking = true;
            _duration = 0;
        }

        public void Stop() {
            IsTicking = false;
            Elapsedtime = 0;
            DeltaTime = 0;
        }

        public void Update() {
            if (IsTicking) {
                if (AutoReset) {
                    if (_duration >= Interval) {
                        _duration -= Interval;
                    }
                } else {
                    if (_duration >= Interval) {
                        Stop();
                    }
                }
                if (IsTicking) {
                    UpdateTimer();
                }
            }
        }

        private void UpdateTimer() {
            DeltaTime = (Time.realtimeSinceStartup - _lastTime)*TimeScale;
            Elapsedtime += DeltaTime;
            NormalizedTime = Elapsedtime/Interval;
            _duration += DeltaTime;
            _lastTime = Time.realtimeSinceStartup;
        }

        #endregion
    }
}