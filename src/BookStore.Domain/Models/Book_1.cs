//using System;
//using System.Collections.Generic;

//namespace BookStore.Domain.Models;

//public partial class Book: Entity
//{
//    public long Id { get; set; }

//    public string Title { get; set; } = null!;

//    public long CategoryId { get; set; }

//    public long PublisherId { get; set; }

//    public virtual BookCategory Category { get; set; } = null!;

//    public virtual Publisher Publisher { get; set; } = null!;

//    public virtual ICollection<Author> Authors { get; } = new List<Author>();
//}
