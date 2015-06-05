#region

using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Core.Event;
using Assets.Scripts.Core.Event.Events;
using Assets.Scripts.Core.Game;
using Assets.Scripts.Core.Loader;
using Assets.Scripts.Core.Utils.Pool;
using UnityEngine;
using Object = UnityEngine.Object;

#endregion

namespace Assets.Scripts.Core.Asset {
    public enum OFAssetType {
        Atlas,
        Units,
        Animations,
        Effects,
    }

    public class OFAssetManager {
        #region Fields

        public const string Suffix = ".assetbundle";
        private static readonly OFAssetManager _instance = new OFAssetManager();
        private readonly Dictionary<string, OFAsset> _assets = new Dictionary<string, OFAsset>();
        private readonly OFLoader _loader = new OFLoader();

        #endregion

        #region Properties

        public static OFAssetManager Instance {
            get { return _instance; }
        }

        #endregion

        #region Methods

        public OFAsset Get(string name) {
            OFAsset asset;
            _assets.TryGetValue(name, out asset);
            if (asset == null) {
                Debug.LogError(name + " asset is missing!");
            }
            return asset;
        }

        public void Load() {
            foreach (string assetType in Enum.GetNames(typeof (OFAssetType))) {
                _loader.Add(assetType, "file://" + Application.streamingAssetsPath + "/" + assetType.ToLower() + Suffix);
            }
            _loader.On(OFEventType.LoadItemComplete, OnItemLoadFromLoaderComplete);
            _loader.Start();
        }

        private IEnumerator LoadItem(OFLoadItem item) {
            AssetBundle assetBundle = item.www.assetBundle;
            AssetBundleRequest request = assetBundle.LoadAllAssetsAsync(typeof (Object));
            yield return request;
            Object[] objects = request.allAssets;
            foreach (Object o in objects) {
                //GameObject gameObject = o as GameObject;
                //if (gameObject != null) {
                OFAsset asset = new OFAsset {Object = o};
                _assets[o.name] = asset;
                Debug.Log(o + "\t" + o.name);
                //}
            }
            //OFDebug.Log(type + " www2: " + www.url + "\t" + www.isDone);
            assetBundle.Unload(false);
            OFLoadEvent loadEvent = OFPool.Instance.Pop<OFLoadEvent>();
            loadEvent.Data = item;
            loadEvent.Type = OFEventType.LoadItemComplete;
            OFGame.Trigger(loadEvent);
            OFPool.Instance.Push(loadEvent);
        }

        private void OnItemLoadFromLoaderComplete(params object[] args) {
            OFLoadEvent loadEvent = args[0] as OFLoadEvent;
            if (loadEvent != null) {
                //OFDebug.Log(loadEvent.Data);
                OFGame.StartGameCoroutine(LoadItem(loadEvent.Data));
            }
        }

        #endregion
    }
}