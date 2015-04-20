using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace TLS
{
    class Hash
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
        ///  A method which handles verifacation of messages
        /// </summary>
        public bool Verify(string message, string sign, BigInteger E, BigInteger N)
        {
            BigInteger Message = new BigInteger(Encoding.UTF8.GetBytes(message));
            BigInteger Sign = new BigInteger(Encoding.UTF8.GetBytes(sign));

            return BigInteger.ModPow(Sign, E, N).Equals(BigInteger.ModPow(Message,1,N));
        }
    }
}
