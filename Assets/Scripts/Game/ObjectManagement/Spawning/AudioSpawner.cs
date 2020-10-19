using General.Behaviours;
using UnityEngine;

namespace Game.ObjectManagement.Spawning
{
    [RequireComponent(typeof(IObjectSpawner))]
    public class AudioSpawner : ExtendedBehaviour
    {
        private IObjectSpawner _spawner;
        
        private void Awake()
        {
            _spawner = GetComponent<IObjectSpawner>();
        }
    }
}