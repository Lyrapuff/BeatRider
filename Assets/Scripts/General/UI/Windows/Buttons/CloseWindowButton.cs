using General.Behaviours;

namespace General.UI.Windows.Buttons
{
    public class CloseWindowButton : ExtendedBehaviour
    {
        public void Close(Window window)
        {
            window.Close();
        }
    }
}