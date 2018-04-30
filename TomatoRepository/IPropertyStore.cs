namespace Tomato.Repository
{
    public interface IPropertyStore
    {
        T Get<T>(object obj, string key) where T : class;
        void Put<T>(object obj, string key, T value) where T : class;
    }
}