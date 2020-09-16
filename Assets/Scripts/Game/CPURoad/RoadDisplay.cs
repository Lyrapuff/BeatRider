using General.Behaviours;
using General.Storage;
using UnityEngine;

namespace Game.CPURoad
{
    [RequireComponent(typeof(RoadHeight))]
    public class RoadDisplay : ExtendedBehaviour
    {
        [SerializeField] private Material _material;

        private RoadHeight _roadHeight;
        
        private static readonly int Offset = Shader.PropertyToID("_Offset");
        private static readonly int Road = Shader.PropertyToID("_Road");

        private void Awake()
        {
            _roadHeight = GetComponent<RoadHeight>();
            
            IStorage storage = FindComponentOfInterface<IStorage>();
            Texture2D road = storage.Get<Texture2D>("Game/RoadTexture");
            
            _material.SetFloat(Offset, _roadHeight.Offset);
            _material.SetTexture(Road, road);
        }

        private void Update()
        {
            _material.SetFloat(Offset, _roadHeight.Offset);
        }
    }
}