using System;
using UnityEngine;

namespace General.Services.Pause
{
    public class NullPause : IPause
    {
        public Action OnPaused { get; set; }
        public Action OnUnpaused { get; set; }
        public bool Paused { get; set; }

        public NullPause()
        {
            Debug.LogError(typeof(NullPause) + " is being used. Please, replace it with a real implementation.");
        }
    }
}