namespace General.UI.Windows
{
    public interface IWindowFactory
    {
        void Open(Window window);
        void CloseAll();
    }
}