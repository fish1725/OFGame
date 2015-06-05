#region

using UnityEditor;

#endregion

namespace Assets.Scripts.Editor {
    public class CreateAssetBundles {
        #region Methods

        [MenuItem("Assets/Build AssetBundles")]
        private static void BuildAllAssetBundles() {
            BuildPipeline.BuildAssetBundles("Assets/StreamingAssets");
        }

        #endregion
    }
}