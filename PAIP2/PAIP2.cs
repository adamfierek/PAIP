using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAIP2
{
    public class PAIP
    {
        public int Factorial(int a)
        {
            var result = 1;

            for (var i = 1; i <= a; i++)
            {
                result *= i; // to samo, co result = result * i
            }

            return result;
        }
        public int Factorial2(int a)
        {
            var result = 1;
            var i = 1;
            while (i <= a)
            {
                result *= i;
                i++;
            }

            return result;
        }
        public bool IsPrime(int a) //Ctrl+K, D <- Formatowanie kodu
        {
            for (var i = 2; i < a; i++)
            {
                if (a % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public void Average()
        {
            Console.WriteLine("Funkcja obliczająca średnią ważoną");
            var input = "";
           
            var weight_sum = 0;
            var sum = 0;

            do
            {
                input = Console.ReadLine();
                var tab = input.Split(' '); // to samo, co (char)32
                if (input == "") break;
                if (tab.Length != 2)
                {
                    Console.WriteLine("Podane wejscie jest nieprawidlowe!");
                    continue;
                }

                int.TryParse(tab[0], out int element);
                int.TryParse(tab[1], out int weight);

                weight_sum = weight_sum + weight; // weight_sum+=weight
                sum = sum + element * weight; //sum+=element * weight

            }
            while (true);

            Console.WriteLine("Srednia wazona: " + (double)sum / weight_sum);

        }

    }
}
