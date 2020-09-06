using System.Collections.Generic;
using General.ObjectManagement.Pooling;
using General.ObjectManagement.Spawning;
using UnityEngine;

namespace Game.ObjectManagement.Spawning.States
{
    public class PatternSpawnerState : SpawnerState
    {
        private SpawnerPattern _pattern;
        private readonly int _duration;
        private int _line;
        
        public PatternSpawnerState(StateMachineSpawner spawner) : base(spawner)
        {
            _pattern = _spawner.Patterns[Random.Range(0, _spawner.Patterns.Length)];
            _duration = _pattern.GetDuration();
        }

        public override void Next()
        {
            Dictionary<int, ObjectType> objects = _pattern.GetObjects(_line);
            
            foreach (KeyValuePair<int, ObjectType> line in objects)
            {
                IObjectPool pool = _spawner.GetPool();
                Transform enemy = pool.Create(line.Value).transform;
                enemy.position = new Vector3(line.Key, 0.5f, 250f);
            }
            
            if (++_line >= _duration)
            {
                _spawner.SetRandomState();
            }
        }
    }
}