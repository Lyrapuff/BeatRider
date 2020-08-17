using System;
using UnityEngine;

namespace General.Services.GameStatus
{
    public class NullGameStatus : IGameStatus
    {
        public Action<GameStatusChangeType, object> OnChanged { get; set; }
        
        public NullGameStatus()
        {
            Debug.LogError(typeof(NullGameStatus) + " is being used. Please, replace it with a real implementation.");
        }
        
        public void Change(GameStatusChangeType changeType, object dataOverload)
        {
            
        }
    }
}