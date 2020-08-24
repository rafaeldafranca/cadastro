using System.Security.Cryptography;
using System.Text;

namespace Cadastro.Util
{
    public static class Hash
    {
        public static string GetCadastroHashCode(this string  value)
        {
            var salt = "!@#$";
            HashAlgorithm _algorithm = MD5.Create();
            var encodedValue = Encoding.UTF8.GetBytes(value + salt);
            var encryptedPassword = _algorithm.ComputeHash(encodedValue);

            var sb = new StringBuilder();
            foreach (var caracter in encryptedPassword)
            {
                sb.Append(caracter.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}

