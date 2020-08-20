using General.Behaviours;
using General.Storage;
using General.Services.GameStatus;
using General.UI.Popups.CanvasManagement;
using Game.Services;
using Game.Services.Implementations;
using UnityEngine;

public class DeathChecker : ExtendedBehaviour
{
    private GameStatusService _gameStatus;
    private ICanvasSwitcher _canvasSwitcher;

    private void Awake()
    {
        _gameStatus = Toolbox.Instance.GetService<GameStatusService>();
        
        _canvasSwitcher = FindComponentOfInterface<ICanvasSwitcher, NullCanvasSwitcher>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "Point(Clone)")
        {
            _gameStatus.Change(GameStatusChangeType.Crushed, null);
            _canvasSwitcher.Open("Continue");

            GameData gameData = GameDataStorage.Instance.GetData();

#if UNITY_ANDROID
            if (gameData.VibrationEnabled)
            {
                Handheld.Vibrate();
            }
#endif
            
        }
    }
}
