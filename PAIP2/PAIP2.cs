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
        public bool Validate(string pesel)
        {
            //sprawdzanie dlugosci
            if (pesel.Length != 11)
            {
                Console.WriteLine("Numer PESEL ma nieprawidlowa dlugosc!");
                return false;
            }

            //sprawdzanie, czy znaki sa cyframi
            for (var i = 0; i < pesel.Length; i++)
            {
                if (pesel[i] < 48 || pesel[i] > 57)
                {
                    Console.WriteLine("Numer PESEL ma nieprawidlowe znaki!");
                    return false;
                }
            }

            //if (pesel.Any(p=>p<48 ||p>57))
            //{
            //    Console.WriteLine("Numer PESEL ma nieprawidlowe znaki!");
            //    return false;
            //}

            var sums = new int[] { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3, 1 };

            var numbers = pesel
                .Select(p => p - 48)
                .ToArray();

            var sum = 0;

            for (var i = 0; i < 11; i++)
            {
                sum += numbers[i] * sums[i];
            }

            if (sum % 10 != 0)
            {
                Console.WriteLine("Numer PESEL jest nieprawidlowy!");
                return false;
            }

            Console.WriteLine("Numer PESEL jest PRAWIDLOWY!");
            return true;
        }
        public string Find(string pesel)
        {
            if (pesel.Length != 11)
            {
                Console.WriteLine("Numer PESEL ma nieprawidlowa dlugosc!");
                return pesel;
            }

            if (pesel.Count(p => p < 48 || p > 57) != 1)
            {
                Console.WriteLine("Nie znaleziono!");
                return pesel;
            }

            var sums = new int[] { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3, 1 };

            var index = pesel.IndexOf(pesel.First(p => p < 48 || p > 57));

            var sum = 0;

            for (var i = 0; i < 11; i++)
            {
                if (i == index) continue;
                sum = sum + sums[i] * (pesel[i] - 48);
            }

            if (sum % 10 == 0)
            {
                pesel = pesel.Replace(pesel[index], '0');
                Console.WriteLine(pesel);
                return pesel;
            }

            for (var i = 1; i <= 9; i++)
            {
                if ((sum + sums[index] * i) % 10 == 0)
                {
                    pesel = pesel.Replace(pesel[index], (char)(i + 48));
                    Console.WriteLine(pesel);
                    return pesel;
                }
            }
            Console.WriteLine("Nie znaleziono");
            return pesel;
        }

        private char Convert(char c, int key)
        {
            if (key > 0)
            {
                for (var i = 0; i < key; i++)
                {
                    c++;
                    if (c > 126) c = (char)32;
                }
            }
            if (key < 0)
            {
                for (var i = 0; i > key; i--)
                {
                    c--;
                    if (c < 32) c = (char)126;
                }
            }

            return c;
        }

        public string Encode(string msg, int key)
        {
            var output = "";
            for(var i=0; i<msg.Length;i++)
            {
                output += Convert(msg[i], key);
            }
            return output;
        }

        public string Decode(string msg, int key)
        {
            var output = "";
            for (var i = 0; i < msg.Length; i++)
            {
                output += Convert(msg[i], -key);
            }
            return output;
        }
    }
}
