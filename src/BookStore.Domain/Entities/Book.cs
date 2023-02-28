using BookStore.Domain.Common;
using System;
using System.Collections.Generic;

namespace BookStore.Domain.Entities;

public partial class Book : BaseEntity
{
    //public long Id { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int? YearBook { get; set; }

    public long CategoryId { get; set; }

    public long PublisherId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Publisher Publisher { get; set; } = null!;
}
