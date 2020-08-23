using System;

namespace General.Inputs
{
    public interface IUIInput
    {
        Action OnPaused { get; set; }
    }
}