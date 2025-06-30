using ClienteCrud.Servicos.Interfaces;

using Newtonsoft.Json;

using StackExchange.Redis;

using System;

namespace ClienteCrud.Infra.Cache
{
    public class CacheRedis : ICacheRedis
    {
        private static readonly Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect("localhost:6379");
        });
        public static ConnectionMultiplexer Connection => lazyConnection.Value;
        private readonly IDatabase _db;

        public CacheRedis()
        {
            _db = Connection.GetDatabase();
        }

        public void Set<T>(string key, T value, TimeSpan? expiry = null)
        {
            var json = JsonConvert.SerializeObject(value);
            var salvou = _db.StringSet(key, json, expiry);

            if (true)
            {

            }
        }

        public T Get<T>(string key)
        {
            var value = _db.StringGet(key);
            return value.HasValue ? JsonConvert.DeserializeObject<T>(value) : default;
        }

        public void Remove(string key)
        {
            _db.KeyDelete(key);
        }
    }
}
