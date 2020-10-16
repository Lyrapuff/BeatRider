using System;
using UnityEngine;

namespace Entities.Movement
{
    public class MovingEntity : MonoBehaviour
    {
        [SerializeField] private MovingEntityData data = new MovingEntityData
        {
            Speed = 1f
        };
        
        private MovementManager _movementManager;

        private void Awake()
        {
            _movementManager = FindObjectOfType<MovementManager>();
        }

        private void OnEnable()
        {
            _movementManager.AddEntity(transform, data);
        }

        private void OnDisable()
        {
            _movementManager.RemoveEntity(transform);
        }
    }

    [Serializable]
    public struct MovingEntityData
    {
        [Range(0, 1)]
        public float Speed;
        [HideInInspector] public Vector3 Position;
        [HideInInspector] public Quaternion Rotation;
    }
}