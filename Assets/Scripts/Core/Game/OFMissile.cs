#region

using UnityEngine;

#endregion

namespace Assets.Scripts.Core.Game {
    public class OFMissile : OFGameObject {
        #region Fields

        private SphereCollider _collider;
        private Rigidbody _rigidbody;

        #endregion

        #region Properties

        public Vector3 Direction { get; set; }
        public float MoveSpeed { get; set; }
        public float Radius { get; set; }

        public OFGameObject Target { get; set; }

        #endregion

        #region Methods

        public void Start() {
            _collider = gameObject.AddComponent<SphereCollider>();
            _collider.radius = Radius;
            _collider.isTrigger = true;
            _rigidbody = gameObject.AddComponent<Rigidbody>();
            _rigidbody.useGravity = false;
            _rigidbody.velocity = new Vector3(0, 0, 10);
            MoveSpeed = 10;
        }

        public void Update() {
            //if (Direction.sqrMagnitude > 0) {
            //    Vector3 pos = transform.position;
            //    pos += MoveSpeed*Direction.normalized*OFGameTime.deltaTime;
            //    transform.position = pos;
            //}
        }

        #endregion
    }
}