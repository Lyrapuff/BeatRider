using System;
using System.Collections.Generic;
using System.Linq;
using Game.ObjectManagement.Pooling;
using General.Tools;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.ObjectManagement.Spawning.Scriptable.Modules
{
    [CreateAssetMenu(menuName = "Object Management/Scriptable/Random module")]
    public class RandomSpawnModule : SpawnModule
    {
        [SerializeField] private int _count = 1;
        [SerializeField] private float _range = 1f;
        [SerializeField] private float _offset = 300f;

        public RandomSpawnOption[] Objects;

        public override void Spawn(IObjectPool pool)
        {
            List<Vector3> positions = new List<Vector3>();
            
            for (int i = 0; i < _count; i++)
            {
                int number = SmallRandom.GetNumber(0, 101);

                float progress = 0;
                
                foreach (RandomSpawnOption option in Objects)
                {
                    progress += option.Fill;

                    if (progress >= number)
                    {
                        pool.RequestAsync(option.PrefabReference, instance =>
                        {
                            Vector3 position;

                            int tries = 0;
                            
                            do
                            { 
                                position = new Vector3(SmallRandom.GetNumber(-_range, _range), 0f, _offset);
                                tries++;
                            } 
                            while (positions.Any(x => Vector3.Distance(x, position) < 2.2f) && tries < 100);

                            positions.Add(position);
                            
                            instance.transform.localPosition = position;
                        });
                        
                        break;
                    }
                }
            }
        }
    }

    [Serializable]
    public class RandomSpawnOption
    {
        public AssetReference PrefabReference;
        [Range(0, 100)]
        public float Fill;
    }
}