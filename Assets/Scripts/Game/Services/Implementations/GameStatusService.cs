using System;
using General.Behaviours;
using General.Services.GameStatus;

namespace Game.Services.Implementations
{
    public class GameStatusService : ExtendedBehaviour
    {
        public GameStatusChangeType Status => _status;
        public Action<GameStatusChangeType, object> OnChanged { get; set; }

        private GameStatusChangeType _status = GameStatusChangeType.Ended;
        
        public void Change(GameStatusChangeType changeType, object dataOverload = null)
        {
            if (_status != changeType)
            {
                _status = changeType;
                OnChanged?.Invoke(_status, dataOverload);
            }
        }
    }
}