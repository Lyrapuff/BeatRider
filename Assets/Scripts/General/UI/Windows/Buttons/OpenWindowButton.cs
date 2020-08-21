using General.Behaviours;

namespace General.UI.Windows.Buttons
{
    public class OpenWindowButton : ExtendedBehaviour
    {
        private IWindowFactory _windowFactory;

        private void Awake()
        {
            _windowFactory = FindComponentOfInterface<IWindowFactory>();
        }

        public void Open(Window window)
        {
            _windowFactory.Open(window);
        }
    }
}