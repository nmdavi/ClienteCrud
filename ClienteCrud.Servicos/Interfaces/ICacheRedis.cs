using System;

namespace ClienteCrud.Servicos.Interfaces
{
    public interface ICacheRedis
    {
        T Get<T>(string key);
        void Remove(string key);
        void Set<T>(string key, T obj, TimeSpan? expiry = null);
    }
}
