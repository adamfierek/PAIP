using PAIP2;
using System;


//https://github.com/kolorowezworki/paip

namespace ConsoleApp001
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var paip = new PAIP();

            //int.TryParse(Console.ReadLine(), out int i);

            //var result = paip.Factorial(i);

            //var result = paip.IsPrime(i);

            //Console.WriteLine(result);

            //paip.Average();

            //paip.Validate("94010901233");
            //paip.Find(Console.ReadLine());
            var msg2 =paip.Encode("Hello, world!", 50);
            Console.WriteLine(msg2);

            var msg1 = paip.Decode(msg2, 50);
            Console.WriteLine(msg1);
        }   
    }
}
