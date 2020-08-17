using System;

namespace General.Services.GameStatus
{
    public interface IGameStatus
    {
        Action<GameStatusChangeType, object> OnChanged { get; set; }

        void Change(GameStatusChangeType changeType, object dataOverload);
    }
}