using System;
using System.Collections.Generic;

namespace BookStore.Domain.Entities;

public partial class Author
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;
}
