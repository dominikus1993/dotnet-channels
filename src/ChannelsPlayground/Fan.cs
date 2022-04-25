using System.Threading.Channels;

namespace ChannelsPlayground;

public static class Fan
{
    public static async ValueTask FanOut<T>(this Channel<T> source, Channel<T>[] dest, CancellationToken cancellationToken = default)
    {
        await foreach (var msg in source.Reader.ReadAllAsync(cancellationToken).WithCancellation(cancellationToken))
        {
            foreach (var channel in dest)
            {
                await channel.Writer.WriteAsync(msg, cancellationToken);
            }
        }
    }
}