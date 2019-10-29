using System;
using System.Security.Cryptography;
using System.Text;
using CocktailMagician.Services.Contracts;

namespace CocktailMagician.Services.Hasher
{
    public class Hasher : IHasher
    {
        string IHasher.Hasher(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}