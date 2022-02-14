using PAIP2;

namespace WebAPI.Services
{
    public class LibraryService
    {
        private PAIP _paip;

        public LibraryService()
        {
            _paip = new PAIP();
        }

        public bool ValidatePesel(string pesel)
        {
            return _paip.Validate(pesel);
        }

        public bool IsPrime(int number)
        {
            return _paip.IsPrime(number);
        }
    }
}
