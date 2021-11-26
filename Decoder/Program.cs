using PAIP2;
using System;

namespace Decoder
{
    class Program
    {
        static void Main(string[] args)
        {
            //[0] - ścieżka do pliku
            //[1] - klucz (liczba)
            //[2] - tryb (litera) e lub d
            //[3] - opcjonalnie ścieżka do pliku docelowego
            var paip = new PAIP();

            if (args.Length < 3)
            {
                Console.WriteLine("Za malo argumentow!");
                return;
            }

            var source = args[0];
            var destination = "result.txt";
            int.TryParse(args[1], out int key);
            var mode = args[2];

            if(args.Length>3)
            {
                destination = args[3];
            }

            var text = paip.ReadFromFile(source);

            switch (mode)
            {
                case "e":
                    {
                        var encoded = paip.Encode(text, key);
                        paip.WriteToFile(encoded, destination);
                        break;
                    }
                case "d":
                    {
                        var decoded = paip.Decode(text, key);
                        paip.WriteToFile(decoded, destination);
                        break;
                    }
            }
             
        }
    }
}
