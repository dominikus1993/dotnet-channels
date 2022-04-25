// See https://aka.ms/new-console-template for more information

using System.Threading.Channels;
using ChannelsPlayground;
var first = Channel.CreateUnbounded<int>();
var second = Channel.CreateUnbounded<int>();
var third = Channel.CreateUnbounded<int>();

for (var i = 0; i < 10; i++)
{
    await first.Writer.WriteAsync(i);
}

await first.FanOut(new[] { second, third });

foreach (var channel in new[] { second, third })
{
    await foreach (var msg in channel.Reader.ReadAllAsync())
    {
        Console.WriteLine(msg);
    }
}

Console.WriteLine("Hello, World!");
