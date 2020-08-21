using System.Collections.Generic;
using System.Linq;
using General.Behaviours;

namespace General.UI.Windows
{
    public class WindowFactory : ExtendedBehaviour, IWindowFactory
    {
        private HashSet<string> _names = new HashSet<string>();
        private List<Window> _instances = new List<Window>();

        public void Open(Window window)
        {
            if (_names.Contains(window.name))
            {
                return;
            }

            Window instance = Instantiate(window, transform);

            instance.OnClosed += () =>
            {
                _names.Remove(window.name);
                _instances.Remove(instance);
            };
            
            _names.Add(window.name);
            _instances.Add(instance);
        }

        public void CloseAll()
        {
            foreach (Window instance in _instances.ToList())
            {
                instance.Close();
            }
        }
    }
}