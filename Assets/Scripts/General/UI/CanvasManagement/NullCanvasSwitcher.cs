using UnityEngine;

namespace General.UI.CanvasManagement
{
    public class NullCanvasSwitcher : ICanvasSwitcher
    {
        public NullCanvasSwitcher()
        {
            Debug.LogError(typeof(NullCanvasSwitcher) + " is being used. Please, replace it with a real implementation.");
        }

        public void Open(string canvasName)
        {
            
        }
    }
}