using General.Behaviours;
using General.SceneManagement;
using General.Storage;
using General.UI.Windows;
using UnityEngine;

namespace Menu.PCUI.GameConfiguration.Buttons
{
    public class StartGameButton : ExtendedBehaviour
    {
        private ISceneChanger _sceneChanger;
        private IWindowFactory _windowFactory;
        private IStorage _storage;

        private void Awake()
        {
            _sceneChanger = FindComponentOfInterface<ISceneChanger>();
            _windowFactory = FindComponentOfInterface<IWindowFactory>();
            _storage = FindComponentOfInterface<IStorage>();
        }

        public void StartGame()
        {
            _storage.Store("seed", Random.Range(-100000, 100000));
            _windowFactory.CloseAll();
            _sceneChanger.Change(SceneType.Game);
        }
    }
}