// See https://aka.ms/new-console-template for more information
using AsyncAnimator.Simulations;

Console.WriteLine("Start");

Sim01Linq.Run();
await Sim02Linq.RunAsync();
await Sim02LinqChunk.RunAsync();
await Sim03AsyncLinq.RunAsync();
await Sim04AsyncExtensions.RunAsync();
await Sim05AsyncPlinq.RunAsync();
Sim06Linq.Run();
await Sim07AsyncPlinq.RunAsync();
await Sim08AsyncPlinq.RunAsync();
await Sim09AsyncOrderPlinq.RunAsync();
await Sim10SelectMany.RunAsync();

Console.WriteLine("Done");
