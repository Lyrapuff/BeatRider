using System;

namespace General.Services.Pause
{
    public interface IPause
    {
        Action OnPaused { get; set; }
        Action OnUnpaused { get; set; }
        bool Paused { get; set; }
    }
}