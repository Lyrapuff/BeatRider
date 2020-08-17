using General.Services.GameStatus;
using Game.Services;
using Game.Services.Implementations;
using UnityEngine;

namespace UI.Buttons
{
    public class ChangeGameStatusButton : MonoBehaviour
    {
        private GameStatusService _gameStatus;

        private void Awake()
        {
            _gameStatus = Toolbox.Instance.GetService<GameStatusService>();
        }

        public void Change(int index)
        {
            _gameStatus.Change((GameStatusChangeType)index);
        }
    }
}