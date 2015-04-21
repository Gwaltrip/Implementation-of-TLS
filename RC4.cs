using System;
using System.Linq;
using System.Text;

namespace TLS
{
    /// <summary>
    ///  A class which handles RC4 encryption and decryption.
    /// </summary>
    class RC4
    {
        private string _key;
        public string Key { set { _key = value; } get { return _key; } }

        private StringBuilder sb;

        /// <summary>
        ///  Default Constructor. If used, must pass Key in by RC4.Key.
        /// </summary>
        public RC4()
        {
            sb = new StringBuilder();
        }

        /// <summary>
        ///  Constuctor which passes in Key.
        /// </summary>
        public RC4(string key)
        {
            sb = new StringBuilder();
            this._key = key;
        }

        /// <summary>
        ///  Creates a random key of the size of 8 bytes.
        /// </summary>
        public void KeyGenerator()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"; 
            Random random = new Random();
            _key = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        ///  Rc4 message method which takes in a decrypted/encrypted string and out puts an encrpyted/decrypted string.
        /// </summary>
        public string Message(string message)
        {
            sb.Clear();
            int temp;
            int mod;
            int next = 0;
            int[] stream = new int[256];

            for (int i = 0; i < 256; i++)
                stream[i] = i;

            for (int i = 0; i < 256; i++)
            {
                next = (_key[i % _key.Length] + stream[i] + next) % 256;
                temp = stream[i];
                stream[i] = stream[next];
                stream[next] = temp;
            }

            for (int i = 0; i < message.Length; i++)
            {
                mod = i % 256;
                next = (stream[mod] + next) % 256;
                temp = stream[mod];
                stream[mod] = stream[next];
                stream[next] = temp;

                sb.Append((char)(message[i] ^ stream[(stream[mod] + stream[next])%256]));
            }
            
            return sb.ToString();
        }
    }
}
