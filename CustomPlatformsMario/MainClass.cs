using CustomFloorPlugin;
using LibSM64;
using SiraUtil.Affinity;
using SM64BS.Behaviours;
using SM64BS.Plugins;
using SM64BS.Utils;
using UnityEngine;
using Zenject;

namespace CustomPlatformsMario
{
    internal class MainClass : SM64BSPlugin, IAffinity
    {
        [Inject] private ResourceUtilities _utils;
        private SM64Mario _sm64Mario;

        [AffinityPrefix]
        [AffinityPatch(typeof(PlatformSpawner), "SpawnPlatform")]
        public void Prefix(ref GameObject platform)
        {
            CollisionAdder collisionAdder = new GameObject("CollisionAdder").AddComponent<CollisionAdder>();
            collisionAdder.main = this;
            collisionAdder.transform.SetParent(platform.transform);
        }

        public override void PluginInitialize()
        {
            SM64Context.SetScaleFactor(2.0f);

        }

        public override void PluginDispose()
        {
            
        }

        public void SpawnMario()
        {
            _sm64Mario = GameScene.SpawnMario(new Vector3(1, 0.02f, 0.75f), Quaternion.identity);
            if (_sm64Mario == null) return;
            _sm64Mario.gameObject.AddComponent<VRInputProvider>();
            _sm64Mario.RefreshInputProvider();

            RaycastShadow shadow = new GameObject("Shadow").AddComponent<RaycastShadow>();
            shadow.utils = _utils;
            shadow.transform.SetParent(_sm64Mario.transform);
            shadow.transform.localPosition = Vector3.zero;
            shadow.transform.localRotation = Quaternion.identity;
        }
    }
}
