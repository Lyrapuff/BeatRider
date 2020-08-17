using General.Behaviours;
using UnityEngine.SceneManagement;

namespace Game.Services.Implementations
{
    public class SceneService : ExtendedBehaviour
    {
        public void LoadScene(int buildIndex)
        {
            SceneManager.LoadScene(buildIndex);
        }
    }
}