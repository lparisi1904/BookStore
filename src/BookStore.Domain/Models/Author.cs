using System;
using System.Collections.Generic;

namespace BookStore.Domain.Models;

public partial class Author
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual AuthorContact? AuthorContact { get; set; }

    public virtual ICollection<Book> Books { get; } = new List<Book>();
}
