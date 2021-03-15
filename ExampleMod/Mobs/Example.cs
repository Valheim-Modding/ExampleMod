using UnityEngine;
using ValheimLib;
using ValheimLib.Spawn;

namespace ExampleMod.Mobs
{
    internal static class Example
    {
        internal static void Init()
        {
            SpawnSystemHelper.OnAfterInit += AddCustomDeerWithBlobMaterial;
        }

        private static void AddCustomDeerWithBlobMaterial(SpawnSystem spawnSystem)
        {
            var origDeerSpawner = spawnSystem.m_spawners[0];

            // cloning the original deer spawner.
            var myCustomSpawner = origDeerSpawner.Clone();

            // making it so that our zone spawner only works in black forests.
            myCustomSpawner.m_biome = Heightmap.Biome.BlackForest;

            // we cloning the original deer prefab so that we don't modify the original.
            myCustomSpawner.m_prefab = origDeerSpawner.m_prefab.InstantiateClone("myCustomDeer");

            // retrieve the mesh renderer and make it so that the deers have the blob material.
            var renderer = myCustomSpawner.m_prefab.GetComponentInChildren<SkinnedMeshRenderer>();
            renderer.material = Prefab.Cache.GetPrefab<GameObject>("Blob").GetComponentInChildren<SkinnedMeshRenderer>().material;

            spawnSystem.m_spawners.Add(myCustomSpawner);
        }
    }
}
