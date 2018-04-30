using System;

namespace Tomato.Repository
{
    public interface IPropertyRepository
    {
        T Get<T>(Guid key, string name) where T : class;

        void Put<T>(Guid key, string name, T value) where T : class;
    }
}