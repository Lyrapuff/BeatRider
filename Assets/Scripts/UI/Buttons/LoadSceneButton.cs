using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Buttons
{
    public class LoadSceneButton : MonoBehaviour
    {
        public void Load(int buildIndex)
        {
            SceneManager.LoadScene(buildIndex);
        }
    }
}