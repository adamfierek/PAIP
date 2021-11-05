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
    }
}
