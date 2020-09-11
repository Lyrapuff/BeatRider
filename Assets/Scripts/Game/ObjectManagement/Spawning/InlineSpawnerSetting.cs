using System;
using UnityEngine;

namespace Game.ObjectManagement.Spawning
{
    [Serializable]
    public class InlineSpawnerSetting
    {
        public GameObject Prefab => _prefab;
        public int Periodicity => _periodicity;
        
        [HideInInspector] public float Chance;
        
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _periodicity;
    }
}