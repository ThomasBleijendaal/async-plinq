// See https://aka.ms/new-console-template for more information
using AsyncAnimator.Simulations;

Console.WriteLine("Start");

Sim1Linq.Run();
await Sim2Linq.RunAsync();
await Sim3AsyncLinq.RunAsync();
await Sim4AsyncExtensions.RunAsync();
await Sim5AsyncPlinq.RunAsync();
Sim6Linq.Run();
await Sim7AsyncPlinq.RunAsync();
await Sim8AsyncPlinq.RunAsync();

// TODO: simulate selectmany
// TODO: simulate where

Console.WriteLine("Done");
