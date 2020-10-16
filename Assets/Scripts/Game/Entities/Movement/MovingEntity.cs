using System;
using UnityEngine;

namespace Entities.Movement
{
    public class MovingEntity : MonoBehaviour
    {
        private MovementManager _movementManager;

        private void Awake()
        {
            _movementManager = FindObjectOfType<MovementManager>();
        }

        private void OnEnable()
        {
            _movementManager.AddEntity(transform);
        }

        private void OnDisable()
        {
            _movementManager.RemoveEntity(transform);
        }
    }
}