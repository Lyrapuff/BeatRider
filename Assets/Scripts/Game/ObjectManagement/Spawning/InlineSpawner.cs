using System.Linq;
using Game.ObjectManagement.Pooling;
using Game.ObjectManagement.Spawning;
using General.Behaviours;
using General.Tools;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.ObjectManagement
{
    public class InlineSpawner : ExtendedBehaviour, IObjectSpawner
    {
        [Header("Dependencies")]
        [SerializeField] private ObjectPool _pool;

        [Header("Settings")] 
        [SerializeField] private float _range;
        [SerializeField] private InlineSpawnerSetting[] _settings;

        private void Awake()
        {
            CalculateChances();
        }

        private void CalculateChances()
        {
            int totalPeriodicity = _settings.Sum(set => set.Periodicity);

            foreach (InlineSpawnerSetting setting in _settings)
            {
                setting.Chance = (float)setting.Periodicity / totalPeriodicity * 100f;
            }
        }

        private AssetReference GetReference()
        {
            int number = SmallRandom.GetNumber(0, 101);

            float progress = 0;
            
            foreach (InlineSpawnerSetting setting in _settings
                .OrderBy(x => x.Chance))
            {
                progress += setting.Chance;

                if (progress >= number)
                {
                    return setting.Reference;
                }
            }
            
            return null;
        }
        
        public void Spawn()
        {
            AssetReference reference = GetReference();

            if (ReferenceEquals(reference, null))
            {
                return;
            }
            
            _pool.RequestAsync(reference, instance =>
            {
                instance.transform.position = transform.position + new Vector3(SmallRandom.GetNumber(-_range, _range), 0f, 0f);
            });
        }
    }
}