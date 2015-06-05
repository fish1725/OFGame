#region

using UnityEngine;

#endregion

namespace Assets.Scripts.Core.Loader {
    public class OFLoadItem {
        #region Properties

        public string name { get; set; }
        public string url { get; set; }
        public WWW www { get; set; }

        #endregion
    }
}