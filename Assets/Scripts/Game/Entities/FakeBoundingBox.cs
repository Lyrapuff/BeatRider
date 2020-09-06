using UnityEngine;

namespace Game.Entities
{
    [RequireComponent(typeof(MeshFilter))]
    public class FakeBoundingBox : MonoBehaviour
    {
        private MeshFilter _meshFilter;
        
        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            Bounds meshBounds = _meshFilter.mesh.bounds;
            meshBounds.size *= 100f;
            _meshFilter.mesh.bounds = meshBounds;
        }
    }
}