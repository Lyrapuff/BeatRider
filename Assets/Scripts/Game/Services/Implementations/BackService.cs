using System;
using System.Collections.Generic;
using General.Behaviours;
using UnityEngine;

namespace Game.Services.Implementations
{
    public class BackService : ExtendedBehaviour
    {
        private readonly Stack<Action> _backHandlers = new Stack<Action>();

        private float _lastTime;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Handle();
            }
        }

        public void Handle()
        {
            if (Time.time - _lastTime < 0.3f)
            {
                return;
            }
            
            if (_backHandlers.Count < 1)
            {
                return;
            }
            
            _backHandlers.Pop()?.Invoke();
            _lastTime = Time.time;
        }

        public void Add(Action handler)
        {
            _backHandlers.Push(handler);
        }

        public void Set(Action handler)
        {
            _backHandlers.Clear();
            Add(handler);
        }

        public void RemoveLast()
        {
            _backHandlers.Pop();
        }
    }
}