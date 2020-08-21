using General.Behaviours;
using UnityEngine.SceneManagement;

namespace General.SceneManagement
{
    public class SceneChanger : ExtendedBehaviour, ISceneChanger
    {
        public void Change(SceneType sceneType)
        {
            SceneManager.LoadScene((int)sceneType);
        }
    }
}