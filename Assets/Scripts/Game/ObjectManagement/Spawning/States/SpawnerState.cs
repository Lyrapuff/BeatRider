namespace Game.ObjectManagement.Spawning.States
{
    public class SpawnerState
    {
        protected StateMachineSpawner _spawner;

        public SpawnerState(StateMachineSpawner spawner)
        {
            _spawner = spawner;
        }

        public virtual void Next()
        {
            
        }
    }
}