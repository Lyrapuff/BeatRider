using General.Behaviours;
using General.Services.GameStatus;
using General.UI.CanvasManagement;
using Game.Services;
using Game.Services.Implementations;

namespace UI.Continue
{
    public class Continue : ExtendedBehaviour
    {
        private GameStatusService _gameStatus;
        private ICanvasSwitcher _canvasSwitcher;

        private void Awake()
        {
            _gameStatus = Toolbox.Instance.GetService<GameStatusService>();
            
            _canvasSwitcher = FindComponentOfInterface<ICanvasSwitcher, NullCanvasSwitcher>();
        }

        public void DoContinue()
        {
            _gameStatus.Change(GameStatusChangeType.Continued, null);
            _canvasSwitcher.Open("Game");
        }
    }
}