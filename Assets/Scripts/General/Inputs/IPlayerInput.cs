using System;

namespace General.Inputs
{
    public interface IPlayerInput
    {
        bool ReadInputs { get; set; }
        Action OnBack { get; set; }
        Action OnPaused { get; set; }
        float Direction { get; }
    }
}