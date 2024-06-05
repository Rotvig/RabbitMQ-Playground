using Rebus.Handlers;
using Shared.Messages;

namespace Consumer;

public class Handler : IHandleMessages<VitroLifeClass>, IHandleMessages<VitroLifeRecord>
{
    public Task Handle(VitroLifeClass message)
    {
        Console.WriteLine("VitroLifeClass Received with message:" + message.StringProperty);
        return Task.CompletedTask;
    }

    public Task Handle(VitroLifeRecord message)
    {
        Console.WriteLine("VitroLifeRecord Received with message:" + message.StringProperty);
        return Task.CompletedTask;
    }
}