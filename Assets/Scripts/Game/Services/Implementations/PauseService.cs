using System;
using General.Behaviours;
using General.Inputs;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Services.Implementations
{
    public class PauseService : ExtendedBehaviour
    {
        public Action OnPaused { get; set; }
        public Action OnUnpaused { get; set; }

        public bool Paused
        {
            get
            {
                return _paused;
            }
            set
            {
                _paused = value;
                
                if (_paused)
                {
                    Debug.Log("yes");
                    OnPaused?.Invoke();
                }
                else
                {
                    Debug.Log("no");
                    OnUnpaused?.Invoke();
                }
            }
        }

        private IPlayerInput _input;
        private bool _paused;

        private void Awake()
        {
            SceneManager.sceneLoaded += (scene, mode) =>
            {
                if (_input != null)
                {
                    _input.OnPaused -= BackHandle;
                }
                
                _input = FindComponentOfInterface<IPlayerInput, NullPlayerInput>();

                _input.OnPaused += BackHandle;
            };
        }

        private void BackHandle()
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                Paused = !Paused;
            }
        }
    }
}