using UnityEngine;

namespace Game.ObjectManagement.Pooling
{
    public class ObjectCleaner : MonoBehaviour
    {
        [SerializeField] private float _clerAt = -10f;

        private void Update()
        {
            if (transform.position.z <= _clerAt)
            {
                gameObject.SetActive(false);
            }
        }
    }
}