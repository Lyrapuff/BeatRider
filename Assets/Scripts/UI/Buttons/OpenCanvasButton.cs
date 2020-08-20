using General.Behaviours;
using General.UI.Popups.CanvasManagement;

namespace UI.Buttons
{
    public class OpenCanvasButton : ExtendedBehaviour
    {
        private ICanvasSwitcher _canvasSwitcher;

        private void Awake()
        {
            _canvasSwitcher = FindComponentOfInterface<ICanvasSwitcher, NullCanvasSwitcher>();
        }

        public void Open(string canvasName)
        {
            _canvasSwitcher.Open(canvasName);
        }
    }
}