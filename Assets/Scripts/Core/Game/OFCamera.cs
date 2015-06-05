#region

using UnityEngine;

#endregion

namespace Assets.Scripts.Core.Game {
    public class OFCamera : OFGameObject {
        #region Fields

        public float distance = 10;
        public float height = 8;
        public float speed = 20;
        public OFGameObject target;
        private Camera _camera;

        public Camera Camera {
            get { return _camera; }
            set { _camera = value; }
        }

        #endregion

        #region Methods

        public void Start() {
            Camera = gameObject.AddComponent<Camera>();
        }

        public void LateUpdate() {
            if (target != null) {
                Vector3 targetPosition = target.TransformPoint(new Vector3(0, height, -distance));
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed*OFGameTime.deltaTime);
                transform.LookAt(target.position);
            }
        }

        #endregion
    }
}