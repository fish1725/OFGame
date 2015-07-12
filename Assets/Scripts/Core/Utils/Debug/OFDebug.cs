#region

using System.Collections.Generic;
using System.Text;
using Assets.Scripts.Core.UI;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Assets.Scripts.Core.Utils.Debug {
    public class OFDebug {
        #region Fields

        public string ErrorTextColor = "red";
        public Font TextFont = Resources.GetBuiltinResource<Font>("Arial.ttf");
        public string logTextColor = "white";
        private static readonly StringBuilder _debugText = new StringBuilder();
        private static readonly Queue<string> _textQueue = new Queue<string>();
        private static Text _text;

        #endregion

        #region Methods

        public void Init() {
            GameObject go = GameObject.Find("DebugCanvas");
            if (go == null) {
                go = new GameObject("DebugCanvas");
                Canvas canvas = go.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                go.AddComponent<CanvasScaler>();
            }
            if (_text != null) {
                Object.Destroy(_text.gameObject);
            }
            GameObject text = new GameObject();
            text.transform.SetParent(go.transform);
            _text = text.AddComponent<Text>();
            _text.font = TextFont;
            _text.transform.SetAsFirstSibling();
            _text.rectTransform.anchorMin = new Vector2(0, 0);
            _text.rectTransform.anchorMax = new Vector2(1, 1);
            _text.rectTransform.offsetMin = new Vector2(0, 0);
            _text.rectTransform.offsetMax = new Vector2(0, 0);

            Application.logMessageReceivedThreaded += LogHandler;
        }

        private static void AddText(object message, string color) {
            if (_textQueue.Count > 10) {
                _textQueue.Dequeue();
            }
            _textQueue.Enqueue("<color=" + color + ">" + message + "</color>");

            foreach (string s in _textQueue) {
                _debugText.AppendLine(s);
            }
//            _text.text = _debugText.ToString();
            _debugText.Remove(0, _debugText.Length);
        }

        private void LogHandler(string condition, string stackTrace, LogType type) {
            string color = logTextColor;
            string text = condition;
            switch (type) {
                case LogType.Error:
                case LogType.Exception:
                    color = ErrorTextColor;
                    text += "\n" + stackTrace;
                    break;
            }
            AddText(text, color);
        }

        #endregion
    }
}