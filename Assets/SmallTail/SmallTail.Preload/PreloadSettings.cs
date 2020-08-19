using UnityEngine;

namespace SmallTail.Preload
{
    [CreateAssetMenu(menuName = "SmallTail/Preload/Settings")]
    public class PreloadSettings : ScriptableObject
    {
        public GameObject[] PreloadedObjects;
    }
}