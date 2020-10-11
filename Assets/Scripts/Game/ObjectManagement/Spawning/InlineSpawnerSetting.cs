using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.ObjectManagement.Spawning
{
    [Serializable]
    public class InlineSpawnerSetting
    {
        public AssetReference Reference;
        public int Periodicity => _periodicity;
        
        [HideInInspector] public float Chance;
        
        [SerializeField] private int _periodicity;
    }
}