using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BlueYonder.Reservations.Models
{
    public class RedisProvider
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Redis"].ConnectionString;
        private static readonly Lazy<IDatabase> LazyCache = new Lazy<IDatabase>(
            () => ConnectionMultiplexer.Connect(ConnectionString).GetDatabase());

        public static IDatabase Cache { get { return LazyCache.Value; } }
    }
}