using System.Collections.Generic;
using System.Linq;
using General.ObjectManagement.Pooling;
using General.ObjectManagement.Spawning;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.World.ObjectManagement.Spawning.States
{
    public class RandomSpawnerState : SpawnerState
    {
        private int _duration;
        
        public RandomSpawnerState(StateMachineSpawner spawner) : base(spawner)
        {
            _duration = Random.Range(7, 10);
        }

        public override void Next()
        {
            if (_duration < 0)
            {
                _spawner.SetRandomState();
            }

            List<Vector3> positions = new List<Vector3>();
            int count = 0;
            
            switch (Configuration.Instance.Traffic)
            {
                case Configuration.TrafficType.Light:
                    count = Random.Range(1, 2);
                    break;
                case Configuration.TrafficType.Medium:
                    count = Random.Range(1, 3);
                    break;
                case Configuration.TrafficType.Heavy:
                    count = Random.Range(1, 4);
                    break;
            }

            for (int i = 0; i < count; i++)
            {
                IObjectPool enemies = _spawner.GetPool();
                
                Transform enemy;

                if (Random.Range(0, 20) < 19)
                {
                    enemy = enemies.Create(ObjectType.Enemy).transform;
                }
                else
                {
                    enemy = enemies.Create(ObjectType.Point).transform;
                }

                Vector3 position = new Vector3(0f, 0.5f, 250f)
                                   + Vector3.right * Random.Range(-9.5f, 9.5f) 
                                   + Vector3.forward * Random.Range(-1f, 1f);

                while (positions.Count > 0 && positions
                    .Select(pos => Vector3.Distance(pos, position))
                    .OrderBy(x => x)
                    .FirstOrDefault() < 1.8f)
                {
                    position = new Vector3(0f, 0.5f, 250f) + Vector3.right * Random.Range(-9.5f, 9.5f);
                }

                enemy.position = position;
                positions.Add(position);
            }

            _duration--;
        }
    }
}