using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.ObjectManagement.Pooling
{
    public interface IObjectPool
    {
        void RequestAsync(AssetReference reference, Action<GameObject> onGot);
    }
}