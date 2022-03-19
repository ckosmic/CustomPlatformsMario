using CustomFloorPlugin.Interfaces;
using IPA.Utilities;
using LibSM64;
using SM64BS.Behaviours;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CustomPlatformsMario
{
    internal class CollisionAdder : MonoBehaviour, INotifyPlatformEnabled
    {
        public MainClass main;

        public void PlatformEnabled(DiContainer container)
        {
            AddStaticTerrainScripts();
            main.SpawnMario();
        }

        public void AddStaticTerrainScripts()
        {
            Transform platform = transform.parent;
            if (platform == null) return;
            foreach (MeshCollider collider in platform.GetComponentsInChildren<MeshCollider>(false))
            {
                SM64StaticTerrain terrain = collider.gameObject.AddComponent<SM64StaticTerrain>();
                terrain.SetField("surfaceType", SM64SurfaceType.NotSlippery);
            }
            SM64Context.RefreshStaticTerrain();
        }
    }
}
