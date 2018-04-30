namespace Tomato.Repository
{
    public interface IRepository<TKey, TVal>
    {
        TVal Get(TKey key);
        void Put(TKey key, TVal value);
    }
}