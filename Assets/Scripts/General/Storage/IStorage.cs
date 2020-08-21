namespace General.Storage
{
    public interface IStorage
    {
        void Store(string key, object data, bool persistant = false);
        T Get<T>(string key) where T : class;
        T GetOrCreate<T>(string key) where T : class, new();
    }
}