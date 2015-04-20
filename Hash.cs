using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
