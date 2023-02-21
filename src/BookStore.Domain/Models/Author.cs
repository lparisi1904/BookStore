using System;
using System.Collections.Generic;

namespace BookStore.Domain.Models;

public partial class Author : Entity
{
    //public long Id { get; set; }

    public string Name { get; set; } = null!;
}
