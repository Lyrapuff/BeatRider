using Game.CPURoad;
using UnityEngine;

namespace Game.Environments.Forest
{
    public class CloudsOffset : MonoBehaviour
    {
        [SerializeField] private Material _material;

        private RoadHeight _roadHeight;
        private static readonly int Offset = Shader.PropertyToID("_Offset");

        private void Start()
        {
            _roadHeight = FindObjectOfType<RoadHeight>();
        }

        private void Update()
        {
            _material.SetFloat(Offset, _roadHeight.Offset);
        }
    }
}