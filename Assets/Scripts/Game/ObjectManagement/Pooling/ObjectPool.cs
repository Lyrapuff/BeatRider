using System;
using System.Collections.Generic;
using System.Linq;
using General.Behaviours;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.ObjectManagement.Pooling
{
    public class ObjectPool : ExtendedBehaviour, IObjectPool
    {
        private readonly Dictionary<string, List<GameObject>> _instances = new Dictionary<string, List<GameObject>>();
        
        public void RequestAsync(AssetReference reference, Action<GameObject> onGot)
        {
            if (!_instances.ContainsKey(reference.AssetGUID))
            {
                _instances[reference.AssetGUID] = new List<GameObject>();
            }

            GameObject instance = _instances[reference.AssetGUID].FirstOrDefault(x => !x.activeSelf);

            if (instance == null)
            {
                reference.InstantiateAsync(transform).Completed += handle =>
                {
                    _instances[reference.AssetGUID].Add(handle.Result);
                    onGot?.Invoke(handle.Result);
                };
            }
            else
            {
                instance.SetActive(true);
                onGot?.Invoke(instance);
            }
        }
    }
}