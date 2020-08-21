using UnityEngine;

namespace SmallTail.Preload
{
    public static class Preload
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void OnLoad()
        {
            PreloadSettings settings = Resources.Load<PreloadSettings>("Objects/Preload/PreloadSettings");

            if (settings != null)
            {
                foreach (GameObject obj in settings.PreloadedObjects)
                {
                    if (obj == null)
                    {
                        continue;
                    }
                    
                    GameObject instance = Object.Instantiate(obj);
                    instance.name = obj.ToString();
                    Object.DontDestroyOnLoad(instance);
                }
            }
        }
    }
}