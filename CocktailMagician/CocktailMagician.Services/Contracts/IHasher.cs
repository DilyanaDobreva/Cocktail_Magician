using System;
namespace CocktailMagician.Services.Contracts
{
    public interface IHasher
    {
        string Hasher(string text);
    }
}
