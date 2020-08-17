using System;
using UnityEngine;

namespace Game
{
    public class FakeBoundingBox : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<MeshFilter>().sharedMesh.bounds = new Bounds(Vector3.zero, Vector3.one * 100);
        }
    }
}