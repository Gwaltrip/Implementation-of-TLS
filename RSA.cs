using System;
using System.Numerics;
using System.Globalization;
using System.Text;
using System.Linq;

namespace TLS
{
    /// <summary>
    ///  A class which handles RSA encryption and decryption.
    /// </summary>
    class RSA
    {
        private BigInteger _N;
        public string N { get { return _N.ToString(); } set { _N = BigInteger.Parse(value); } }

        private BigInteger _M;
        public string M { get { return _M.ToString(); } set { _M = BigInteger.Parse(value); } }

        private BigInteger _E;
        public string E { get { return _E.ToString(); } set { _E = BigInteger.Parse(value); } }

        private BigInteger _D;
        public string D { get { return _D.ToString(); } set { _D = BigInteger.Parse(value); } }

        private BigInteger _P;
        public string P { get { return _P.ToString(); } set { _P = BigInteger.Parse(value); } }

        private BigInteger _Q;
        public string Q { get { return _Q.ToString(); } set { _Q = BigInteger.Parse(value); } }

        /// <summary>
        ///  Default constructor.
        /// </summary>
        public RSA()
        {

        }

        /// <summary>
        ///  Takes in Modulus, P, and Q in HexString form.
        /// </summary>
        public RSA(string modulus, string pString, string qString)
        {
            _N = BigInteger.Parse(modulus, NumberStyles.HexNumber);
            _P = BigInteger.Parse(pString, NumberStyles.HexNumber);
            _Q = BigInteger.Parse(qString, NumberStyles.HexNumber);
            _M = BigInteger.Multiply((_P - BigInteger.One), (_Q - BigInteger.One));
            _E = new BigInteger(3);
            while (BigInteger.GreatestCommonDivisor(_M, _E) > 1)
            {
                _E = BigInteger.Add(_E, 2);
            }
            _D = modInverse(_E, _M);
        }

        /// <summary>
        ///  Takes in Modulus as HexString. Takes in P, and Q in Int64.
        /// </summary>
        public RSA(string modulus, Int64 p, Int64 q)
        {
            _N = BigInteger.Parse(modulus, NumberStyles.HexNumber);
            _P = new BigInteger(p);
            _Q = new BigInteger(q);
            _M = BigInteger.Multiply((_P - BigInteger.One), (_Q - BigInteger.One));
            _E = new BigInteger(3);
            while (BigInteger.GreatestCommonDivisor(_M, _E) > 1)
            {
                _E = BigInteger.Add(_E, 2);
            }
            _D = modInverse(_E, _M);
        }

        /// <summary>
        ///  Takes in Moudulus and E as String values of ints.
        /// </summary>
        public RSA(string modulus, string eString)
        {
            _N = BigInteger.Parse(modulus);
            _E = BigInteger.Parse(eString);
        }

        /// <summary>
        ///  Takes in Moudulus and E as Int64.
        /// </summary>
        public RSA(Int64 modulus, Int64 eString)
        {
            _N = new BigInteger(modulus);
            _E = new BigInteger(eString);
        }

        /// <summary>
        ///  Finds the mod Inverse of a and b.
        /// </summary>
        public BigInteger modInverse(BigInteger a, BigInteger b)
        {

            BigInteger i = b, v = 0, d = 1;
            while (a > 0)
            {
                BigInteger t = i / a, x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= b;
            if (v < 0)
                v = (v + b) % b;
            return v;
        }

        /// <summary>
        ///  Encrypts the string using RSA and outputs a big integer value as a string.
        /// </summary>
        public string Encrypt(string message)
        {
            BigInteger PlainText = new BigInteger(Encoding.UTF8.GetBytes(message)); 

            return BigInteger.ModPow(PlainText, _E, _N).ToString();
        }

        /// <summary>
        ///  Decrypts the string using RSA and outputs an UTF8 string.
        /// </summary>
        public string Decrypt(string message)
        {
            BigInteger CypherText = BigInteger.Parse(message);

            return Encoding.UTF8.GetString(BigInteger.ModPow(CypherText, _D, _N).ToByteArray());
        }
    }
}
