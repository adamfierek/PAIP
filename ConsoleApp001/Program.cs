using PAIP2;
using System;

namespace ConsoleApp001
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var paip = new PAIP();

            int.TryParse(Console.ReadLine(), out int i);

            //var result = paip.Factorial(i);

            var result = paip.IsPrime(i);

            Console.WriteLine(result);
            
        }
    }
}
