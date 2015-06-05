#region

using Assets.Scripts.Core.Utils.Debug;
using UnityEngine;

#endregion

namespace Assets.Scripts.Core.Game {
    public class OFInputManager {
        #region Fields

        private static readonly OFInputManager _instance = new OFInputManager();

        #endregion

        #region Constructors

        public OFInputManager() {
            Motion = new Vector3();
        }

        #endregion

        #region Properties

        public static OFInputManager Instance {
            get { return _instance; }
        }

        public Vector3 Motion { get; set; }

        #endregion

        #region Methods

        public void Update() {
            Motion = Vector3.zero;
            if (Input.GetKey(KeyCode.W)) {
                Motion += Vector3.forward;
            }
            if (Input.GetKey(KeyCode.S)) {
                Motion += Vector3.back;
            }
            if (Input.GetKey(KeyCode.A)) {
                Motion += Vector3.left;
            }
            if (Input.GetKey(KeyCode.D)) {
                Motion += Vector3.right;
            }
            if (Motion.sqrMagnitude > 0) {
                Motion.Normalize();
                foreach (OFUnit unit in OFGame.LocalPlayer.ControlUnits) {
                    unit.motion = Motion;
                }
            }
        }

        #endregion
    }
}