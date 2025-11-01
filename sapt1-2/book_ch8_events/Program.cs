using System;
using Events;

Engine e1 = new Engine();
e1.Start();
e1.StatusChange += EventHandler;
e1.Start();
e1.Stop();
e1.StatusChange -= EventHandler;
e1.Stop();

static void EventHandler(object sender, EngineEventArgs args)
{
    Console.WriteLine($"{sender.GetType().Name} changed to {args.Status}");
}

