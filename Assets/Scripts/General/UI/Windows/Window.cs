using System;
using General.Behaviours;

namespace General.UI.Windows
{
    public class Window : ExtendedBehaviour
    {
        public Action OnClosed { get; set; }

        public void Close()
        {
            OnClosed?.Invoke();
        }
    }
}