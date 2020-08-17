using General.Behaviours;
using General.Services.GameStatus;
using UnityEngine;

namespace Game.World
{
    public class EnableOnStart : ExtendedBehaviour
    {
        [SerializeField] private MonoBehaviour _component;

        private IGameStatus _gameStatus;

        private void Awake()
        {
            _gameStatus = FindComponentOfInterface<IGameStatus, NullGameStatus>();

            _gameStatus.OnChanged += HandleGameStatusChanged;
        }

        private void HandleGameStatusChanged(GameStatusChangeType changeType, object dataOverload)
        {
            switch (changeType)
            {
                case GameStatusChangeType.Started:
                    _component.enabled = true;
                    break;
                case GameStatusChangeType.Ended:
                    _component.enabled = false;
                    break;
            }
        }
    }
}