using System.Linq;
using Game.ObjectManagement.Pooling;
using UnityEngine;

namespace Game.ObjectManagement.Spawning.StateMachine.States
{
    [CreateAssetMenu(menuName = "Object Management/Spawning States/Random cars")]
    public class RandomCarsState : SpawnerState
    {
        [SerializeField] private float _offset;
        [SerializeField] private float _range;
        
        public override void Spawn(IObjectPool pool)
        {
            int count = Random.Range(1, 4);

            Vector3[] positions = new Vector3[count];
            
            for (int i = 0; i < count; i++)
            {
                GameObject prefab = Prefabs[Random.Range(0, Prefabs.Length)];
                
                Transform instance = pool.Get(prefab).transform;

                Vector3 position;

                do
                {
                    position = new Vector3(Random.Range(-_range, _range), 0f, _offset);
                } 
                while (positions.Any(pos => Vector3.Distance(pos, position) < 1.5f));

                positions[i] = position;
                
                instance.position = position;
            }
        }
    }
}