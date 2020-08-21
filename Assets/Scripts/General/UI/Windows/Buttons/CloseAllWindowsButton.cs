using General.Behaviours;

namespace General.UI.Windows.Buttons
{
    public class CloseAllWindowsButton : ExtendedBehaviour
    {
        private IWindowFactory _windowFactory;

        private void Awake()
        {
            _windowFactory = FindComponentOfInterface<IWindowFactory>();
        }

        public void CloseAll()
        {
            _windowFactory.CloseAll();
        }
    }
}