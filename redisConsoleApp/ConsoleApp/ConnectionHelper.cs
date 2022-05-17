using StackExchange.Redis;
using System;

public class ConnectionHelper
{
    static ConnectionHelper()
    {
        _lazyConnection = new Lazy<ConnectionMultiplexer>(() => 
        {
            return ConnectionMultiplexer.Connect("localhost:6379");
        });
    }

    private static readonly Lazy<ConnectionMultiplexer> _lazyConnection;

    public static ConnectionMultiplexer Connection => _lazyConnection.Value;
}