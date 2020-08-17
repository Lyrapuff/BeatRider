using General.Behaviours;
using UnityEngine;

namespace Game.World
{
    [RequireComponent(typeof(RoadMovement))]
    public class RoadHeight : ExtendedBehaviour
    {
        [Header("Sine wave settings")]
        [SerializeField] private float _sineFrequency;
        [SerializeField] private float _sineAmplitude;

        private RoadMovement _roadMovement;

        private void Awake()
        {
            _roadMovement = GetComponent<RoadMovement>();
        }

        public float GetHeight(float x)
        {
            float sine = Mathf.Sin(x / _sineFrequency + _roadMovement.Offset) * _sineAmplitude;
            return sine;
        }
    }
}