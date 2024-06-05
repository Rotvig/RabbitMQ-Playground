// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Rebus.Bus;
using Rebus.Config;
using Shared.Messages;

Console.WriteLine("Hello, Publisher!");

var services = new ServiceCollection();
services.AddRebus(x => 
    x.Transport(t => 
        t.UseRabbitMqAsOneWayClient("amqp://localhost")    
            .SetPublisherConfirms(true)
            .InputQueueOptions(queueConfig => queueConfig.AddArgument("x-queue-type", "quorum"))
        ));

var serviceProvider = services.BuildServiceProvider();
var bus = serviceProvider.GetService<IBus>();

var counter = 0;
while (true)
{
    await Task.Delay(1000);
    await bus.Publish(new VitroLifeClass { StringProperty = "VitroLifeClass: " + counter});
    await bus.Publish(new VitroLifeRecord("VitroLifeRecord: " + counter));
    counter++;
}
