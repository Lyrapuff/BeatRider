namespace General.SceneManagement
{
    public interface ISceneChanger
    {
        void Change(SceneType sceneType);
    }

    public enum SceneType
    {
        Menu = 0,
        Game = 1
    }
}