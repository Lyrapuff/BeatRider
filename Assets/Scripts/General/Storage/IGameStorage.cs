namespace General.Storage
{
    public interface IGameStorage
    {
        void Store(string key, object data, bool persistant = false);
        T Get<T>(string key) where T : class;
    }
}