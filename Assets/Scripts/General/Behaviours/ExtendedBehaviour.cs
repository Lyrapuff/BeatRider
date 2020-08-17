using System.Linq;
using UnityEngine;

namespace General.Behaviours
{
    public class ExtendedBehaviour : MonoBehaviour
    {
        protected T GetComponent<T, N>() where N : T, new()
        {
            T component = GetComponent<T>();

            if (component == null)
            {
                component = new N();
            }

            return component;
        }

        protected T FindComponentOfInterface<T, N>() where N : T, new()
        {
            T component = FindObjectsOfType<ExtendedBehaviour>().OfType<T>().FirstOrDefault();

            if (component == null)
            {
                component = new N();
            }

            return component;
        }
        
        

        protected T FindComponentOfInterface<T>()
        {
            T component = FindObjectsOfType<ExtendedBehaviour>().OfType<T>().FirstOrDefault();

            return component;
        }
    }
}