using System.Numerics;
using System.Text;

namespace TLS
{
    class Authentication
    {
        /// <summary>
        ///  A very simple hash which will only be used to show hashing.
        /// </summary>
        public string Xor(string input)
        {
            byte[] halfA = Encoding.UTF8.GetBytes(input.Substring(0, input.Length / 2));
            byte[] halfB = Encoding.UTF8.GetBytes(input.Substring(input.Length / 2 + 1, input.Length));
            byte[] hash = new byte[halfA.Length];

            for (int i = 0; i < hash.Length; i++)
            {
                hash[i] = (byte)(halfA[i] ^ halfB[i]);
            }

            return Encoding.UTF8.GetString(hash);
        }

        /// <summary>
        ///  A method which handles Signing of messages.
        /// </summary>
        public string Sign(string input, BigInteger D, BigInteger N)
        {
            BigInteger Input = new BigInteger(Encoding.UTF8.GetBytes(input));

            return BigInteger.ModPow(Input, D, N).ToString();
        }

        /// <summary>
        ///  A method which handles Signing of messages.
        /// </summary>
        public string Sign(string input, string D, string N)
        {
            BigInteger Input = new BigInteger(Encoding.UTF8.GetBytes(input));

            return BigInteger.ModPow(Input, BigInteger.Parse(D), BigInteger.Parse(N)).ToString();
        }

        /// <summary>
        ///  A method which handles verifacation of messages
        /// </summary>
        public bool Verify(string message, string sign, string E, string N)
        {
            BigInteger Message = new BigInteger(Encoding.UTF8.GetBytes(message));
            BigInteger Sign = BigInteger.Parse(sign);

            return (BigInteger.ModPow(Message, 1, BigInteger.Parse(N))) == BigInteger.ModPow(Sign, BigInteger.Parse(E), BigInteger.Parse(N));
        }
    }
}
