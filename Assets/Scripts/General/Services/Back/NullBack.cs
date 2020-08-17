using System;
using UnityEngine;

namespace General.Services.Back
{
    public class NullBack : IBack
    {
        public NullBack()
        {
            Debug.LogError(typeof(NullBack) + " is being used. Please, replace it with a real implementation.");
        }
        
        public void Handle()
        {
            
        }

        public void Add(Action handler)
        {
            
        }

        public void Set(Action handler)
        {
            
        }

        public void RemoveLast()
        {
            
        }
    }
}