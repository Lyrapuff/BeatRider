using System;
using General.Behaviours;

namespace Game.Services.Implementation
{
    public class PauseService : ExtendedBehaviour, IPause
    {
        public Action OnPaused { get; set; }
        public Action OnUnpaused { get; set; }
        public bool Paused { get; private set; }
        
        public void Pause()
        {
            if (!Paused)
            {
                Paused = true;
                OnPaused?.Invoke();
            }
        }

        public void Unpause()
        {
            if (Paused)
            {
                Paused = false;
                OnUnpaused?.Invoke();
            }
        }

        public void Switch()
        {
            if (Paused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }
}