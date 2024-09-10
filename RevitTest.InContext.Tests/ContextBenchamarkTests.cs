using Autodesk.Revit.UI;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RevitTest.InContext.Tests
{
    public abstract class ContextBenchamarkTests
    {
        protected UIApplication uiapp;
        public abstract bool Run(UIApplication uiapp);

        [OneTimeSetUp]
        public void Setup(UIApplication uiapp)
        {
            this.uiapp = uiapp;
        }

        [TestCase(10000, ExpectedResult = true)]
        public bool TestContext(int length)
        {
            Console.WriteLine(Run(uiapp));
            var time = BenchamarkRunner.RunMedianMilliseconds(length, () =>
            {
                Run(uiapp);
            });
            Console.WriteLine($"Time: {time:0.0000} ms");
            return Run(uiapp);
        }

        [TestCase(10000, ExpectedResult = false)]
        public bool TestNoContext(int length)
        {
            var task = Task.Run(async () =>
            {
                await Task.Delay(0);
                Console.WriteLine(Run(uiapp));
                var time = BenchamarkRunner.RunMedianMilliseconds(length, () =>
                {
                    Run(uiapp);
                });
                Console.WriteLine($"Time: {time:0.0000} ms");
                return Run(uiapp);
            });
            return task.GetAwaiter().GetResult();
        }

        public static class BenchamarkRunner
        {
            private const int coldLength = 100;
            public static Stopwatch Run(int length, Action action)
            {
                for (int i = 0; i < coldLength; i++)
                {
                    action?.Invoke();
                }

                var stopwatch = Stopwatch.StartNew();
                for (int i = 0; i < length; i++)
                {
                    action?.Invoke();
                }
                stopwatch.Stop();
                return stopwatch;
            }

            public static double RunMedianMilliseconds(int length, Action action)
            {
                var stopwatch = Run(length, action);
                return stopwatch.Elapsed.TotalMilliseconds / length;
            }
        }

    }
}
