using BookStore.Domain.Common;
using System;
using System.Collections.Generic;

namespace BookStore.Domain.Entities;

public partial class Author : BaseEntity
{
    //public long Id { get; set; }

    public string Name { get; set; } = null!;
}
