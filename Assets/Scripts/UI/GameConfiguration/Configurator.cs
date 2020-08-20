using General.Behaviours;
using General.Services.GameStatus;
using General.UI.Popups.CanvasManagement;
using Game;
using Game.Services;
using Game.Services.Implementations;

namespace UI.GameConfiguration
{
    public class Configurator : ExtendedBehaviour
    {
        private SceneService _scene;
        private GameSettingsService _gameSettings;
        private GameStatusService _gameStatus;
        
        private ICanvasSwitcher _canvasSwitcher;
        private Configuration _configuration;

        private void Awake()
        {
            _scene = Toolbox.Instance.GetService<SceneService>();
            _gameStatus = Toolbox.Instance.GetService<GameStatusService>();
            _gameSettings = Toolbox.Instance.GetService<GameSettingsService>();
            
            _canvasSwitcher = FindComponentOfInterface<ICanvasSwitcher, NullCanvasSwitcher>();
            _configuration = new Configuration();
            Configuration.Instance = _configuration;
        }

        public void UpdateSpeed(int gameSpeed)
        {
            _configuration.GameSpeed = (Configuration.GameSpeedType) gameSpeed;
            Configuration.Instance = _configuration;
            StartGame();
        }

        public void UpdateTraffic(int traffic)
        {
            _configuration.Traffic = (Configuration.TrafficType) traffic;
            Configuration.Instance = _configuration;
        }

        public void StartGame()
        {
            _gameSettings.Configuration = _configuration;
            _gameStatus.Change(GameStatusChangeType.Started, _configuration);
            _scene.LoadScene(1);
        }
    }
}