using General.Behaviours;
using General.SceneManagement;
using General.UI.Windows;

namespace Menu.PCUI.GameConfiguration.Buttons
{
    public class StartGameButton : ExtendedBehaviour
    {
        private ISceneChanger _sceneChanger;
        private IWindowFactory _windowFactory;

        private void Awake()
        {
            _sceneChanger = FindComponentOfInterface<ISceneChanger>();
            _windowFactory = FindComponentOfInterface<IWindowFactory>();
        }

        public void StartGame()
        {
            _windowFactory.CloseAll();
            _sceneChanger.Change(SceneType.Game);
        }
    }
}