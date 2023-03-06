﻿using System.ComponentModel.DataAnnotations;

namespace BookStore.SPA.Models
{
    public class Book
    {


        //public string Title { get; set; } = null!;
        //public string Author { get; set; } = null!;

        //public string Description { get; set; } = null!;

        //public int? YearBook { get; set; }

        //public long CategoryId { get; set; }

        //public long PublisherId { get; set; }

        //public virtual Category Category { get; set; } = null!;

        //public virtual Publisher Publisher { get; set; } = null!;



        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(150, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string Title { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(150, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string Author { get; set; }

        public string Description { get; set; }

        public int? YearBook { get; set; }

        [Required(ErrorMessage = "The field Category is required")]
        [Range(1, int.MaxValue, ErrorMessage = "The field Category is required")]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}