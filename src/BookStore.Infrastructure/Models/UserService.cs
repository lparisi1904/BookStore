﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace BookStore.Infrastructure.Models
{
    public partial class UserService
    {
        public int UserId { get; set; }
        public int ServiceId { get; set; }

        public virtual Service Service { get; set; }
        public virtual User User { get; set; }
    }
}