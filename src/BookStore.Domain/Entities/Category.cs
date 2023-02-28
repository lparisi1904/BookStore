using BookStore.Domain.Common;
using System;
using System.Collections.Generic;

namespace BookStore.Domain.Entities;

public partial class Category: BaseEntity
{
    //public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Book> Books { get; } = new List<Book>();
}
