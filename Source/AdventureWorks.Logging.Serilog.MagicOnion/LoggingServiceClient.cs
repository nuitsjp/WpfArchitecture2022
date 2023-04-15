using AdventureWorks.MagicOnion.Client;
using Grpc.Core;
using MagicOnion;

namespace AdventureWorks.Logging.Serilog.MagicOnion;

public class LoggingServiceClient : ILoggingService
{
    public static IMagicOnionClientFactory MagicOnionClientFactory { get; set; } = new NullMagicOnionClientFactory();

    private class NullMagicOnionClientFactory : IMagicOnionClientFactory
    {
        public T Create<T>() where T : IService<T>
        {
            return default!;
        }
    }

    public ILoggingService WithOptions(CallOptions option) => this;

    public ILoggingService WithHeaders(Metadata headers) => this;

    public ILoggingService WithDeadline(DateTime deadline) => this;

    public ILoggingService WithCancellationToken(CancellationToken cancellationToken) => this;

    public ILoggingService WithHost(string host) => this;

    public async UnaryResult RegisterAsync(LogDto logRecord)
    {
        var service = MagicOnionClientFactory.Create<ILoggingService>();
        await service.RegisterAsync(logRecord);
    }
}