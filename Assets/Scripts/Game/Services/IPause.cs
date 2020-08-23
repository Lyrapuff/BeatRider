using System;

namespace Game.Services
{
    public interface IPause
    {
        Action OnPaused { get; set; }
        Action OnUnpaused { get; set; }
        
        bool Paused { get; }

        void Pause();
        void Unpause();
        void Switch();
    }
}