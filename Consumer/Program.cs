// See https://aka.ms/new-console-template for more information

using Consumer;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Bus;
using Rebus.Config;
using Shared.Messages;

Console.WriteLine("Hello, Consumer!");

var services = new ServiceCollection();
services.AddRebus(x => 
    x.Transport(t => 
        t.UseRabbitMq("amqp://localhost", "VitrolifeTestQueue")
            .SetPublisherConfirms(true)
            .InputQueueOptions(queueConfig => queueConfig.AddArgument("x-queue-type", "quorum"))
        ));

services.AutoRegisterHandlersFromAssemblyOf<Program>();

var serviceProvider = services.BuildServiceProvider();
var bus = serviceProvider.GetService<IBus>();
await bus.Subscribe<VitroLifeClass>();        
await bus.Subscribe<VitroLifeRecord>();
Console.ReadLine();
