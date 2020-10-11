using System.Collections.Generic;
using System.Linq;
using Game.ObjectManagement.Pooling;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.ObjectManagement.Spawning.StateMachine.States
{
    [CreateAssetMenu(menuName = "Object Management/Spawning States/Random")]
    public class RandomState : SpawnerState
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _range;
        
        public override void Spawn(IObjectPool pool)
        {
            int count = Random.Range(1, 3);

            List<Vector3> positions = new List<Vector3>();
            
            for (int i = 0; i < count; i++)
            {
                AssetReference reference = References[Random.Range(0, References.Length)];
                
                pool.RequestAsync(reference, instance =>
                {
                    Vector3 position;

                    do
                    {
                        position = new Vector3(Random.Range(-_range, _range), 0f, 0f) + _offset;
                    } 
                    while (positions.Any(pos => Vector3.Distance(pos, position) < 1.5f));

                    positions.Add(position);
                
                    instance.transform.position = position;
                });
            }
        }
    }
}