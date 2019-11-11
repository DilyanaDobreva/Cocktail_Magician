using CocktailMagician.Data.Models;
using CocktailMagician.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services.Contracts.Factories
{
    public interface IBarFactory
    {
        Bar Create(string name, string imageURL, AddressDTO address);
    }
}
