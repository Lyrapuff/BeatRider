using System;
using General.Behaviours;
using General.Storage;
using UnityEngine;

namespace Game.Player
{
    public class PointIncrementer : ExtendedBehaviour
    {
        public static Action OnPointIncremented { get; set; }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "Player")
            {
                GameData gameData = GameDataStorage.Instance.GetData();
                gameData.Points++;
                GameDataStorage.Instance.SetData(gameData);

                gameObject.SetActive(false);
                OnPointIncremented?.Invoke();
            }
        }
    }
}