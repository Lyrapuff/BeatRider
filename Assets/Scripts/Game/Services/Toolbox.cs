using General.Behaviours;
using Game.Services.Implementations;

namespace Game.Services
{
    public class Toolbox : SingletonBehaviour<Toolbox>
    {
        public T GetService<T>() where T : ExtendedBehaviour
        {
            return GetComponent<T>();
        }

        public void AddService<T>() where T : ExtendedBehaviour
        {
            gameObject.AddComponent<T>();
        }
        
        private void Awake()
        {
            name = "Toolbox";
            
            AddService<BackService>();
            AddService<GameStatusService>();
            AddService<PauseService>();
            AddService<TrackProcessorService>();
            AddService<GameSettingsService>();
            AddService<SceneService>();
        }
    }
}