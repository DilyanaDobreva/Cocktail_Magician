﻿using System;
using System.Collections.Generic;

namespace CocktailMagician.Data.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
