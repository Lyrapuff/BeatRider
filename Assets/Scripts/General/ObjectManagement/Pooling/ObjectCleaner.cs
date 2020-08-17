using UnityEngine;

namespace General.ObjectManagement.Pooling
{
    public class ObjectCleaner : MonoBehaviour
    {
        [SerializeField] private float _clearAt;

        private void Update()
        {
            if (transform.position.z <= _clearAt)
            {
                gameObject.SetActive(false);
            }
        }
    }
}