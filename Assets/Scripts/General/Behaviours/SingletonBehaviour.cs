using UnityEngine;

namespace General.Behaviours
{
    public class SingletonBehaviour<T> : ExtendedBehaviour where T : ExtendedBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    _instance = go.AddComponent<T>();
                    DontDestroyOnLoad(go);
                }

                return _instance;
            }
        }
    }
}