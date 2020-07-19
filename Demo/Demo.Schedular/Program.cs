using System;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Schedular
{
    class Program
    {
        static void Main(string[] args)
        {
            for (var i = 0; i < 10; i++)
            {
                var i1 = i;

                Task.Factory.StartNew(() =>
                  {
                      Thread.Sleep(2000);
                      Console.WriteLine("Hello Task library! From: {0}", i1);
                  });
            }

            var a = Console.ReadLine();
            if (a != null && a.Equals("Exit", StringComparison.OrdinalIgnoreCase))
            {
                Environment.Exit(0);
            }
        }
    }
}
