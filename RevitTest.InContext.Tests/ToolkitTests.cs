using Nice3point.Revit.Toolkit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RevitTest.InContext.Tests
{
    public class ToolkitTests
    {
        private const int ColdStartIterations = 200;
        private const int TargetIterations = 3000;
        private readonly Stopwatch _monitor = new();

        [Test]
        public void Execute()
        {
            List<Func<bool>> targets =
            [
                IsRevitInApiMode,
                IsAddinActive,
                IsIdling
            ];

            Console.WriteLine();
            Console.WriteLine("Inside context");

            ExecuteTargets(targets);

            Console.WriteLine();
            Console.WriteLine("Outside context");

            Task.Run(() => ExecuteTargets(targets)).GetAwaiter().GetResult();
        }

        private void ExecuteTargets(List<Func<bool>> targets)
        {
            foreach (var target in targets)
            {
                RunBenchmark(ColdStartIterations, target);

                _monitor.Start();
                RunBenchmark(TargetIterations, target);
                _monitor.Stop();

                Console.WriteLine($"{target.Method.Name}: \t{(_monitor.Elapsed.TotalMilliseconds / TargetIterations):0.0000} ms.");
                _monitor.Reset();
            }
        }

        private static void RunBenchmark(int iterations, Func<bool> target)
        {
            for (var i = 0; i < iterations; i++)
            {
                try
                {
                    target.Invoke();
                }
                catch
                {
                    //ignored
                }
            }
        }

        private static bool IsRevitInApiMode()
        {
            return Context.IsRevitInApiMode;
        }

        private static bool IsAddinActive()
        {
            return Context.UiApplication.ActiveAddInId is not null;
        }

        private static bool IsIdling()
        {
            try
            {
                Context.UiApplication.Idling += OnApplicationIdling;
                Context.UiApplication.Idling -= OnApplicationIdling;
                return true;

                void OnApplicationIdling(object sender, Autodesk.Revit.UI.Events.IdlingEventArgs e)
                {
                }
            }
            catch
            {
                return false;
            }
        }
    }

}
