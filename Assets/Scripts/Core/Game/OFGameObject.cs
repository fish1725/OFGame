#region

using UnityEngine;

#endregion

namespace Assets.Scripts.Core.Game {
    public static class GameObjectEx {
        #region Methods

        /// <summary>
        ///     Destroy all the children.
        /// </summary>
        /// <param name="gameObject"></param>
        public static void DestroyChildren(this GameObject gameObject) {
            foreach (Transform child in gameObject.transform) {
                Object.Destroy(child.gameObject);
            }
        }

        /// <summary>
        ///     Set parent and the same layer.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="parent"></param>
        public static void SetParent(this GameObject gameObject, GameObject parent) {
            gameObject.transform.SetParent(parent.transform);
            gameObject.layer = parent.layer;
        }

        #endregion
    }

    public class OFGameObject : MonoBehaviour {
        #region Properties

        public Vector3 localPosition {
            get { return transform.localPosition; }
            set { transform.localPosition = value; }
        }

        public Vector3 position {
            get { return transform.position; }
            set { transform.position = value; }
        }

        #endregion

        #region Methods

        public virtual void Awake() {
        }

        public virtual void LateUpdate() {
        }

        public Vector3 TransformPoint(Vector3 pos) {
            return transform.TransformPoint(pos);
        }

        #endregion
    }
}