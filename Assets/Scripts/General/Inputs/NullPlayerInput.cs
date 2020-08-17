using System;
using UnityEngine;

namespace General.Inputs
{
    public class NullPlayerInput : IPlayerInput
    {
        public bool ReadInputs { get; set; }
        public Action OnBack { get; set; }
        public Action OnPaused { get; set; }
        public float Direction { get; }

        public NullPlayerInput()
        {
            Debug.LogError(typeof(NullPlayerInput) + " is being used. Please, replace it with a real implementation.");
        }
    }
}