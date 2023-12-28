﻿using System.ComponentModel.DataAnnotations;

namespace fastwin.Entities
{
    public abstract class BaseEntity 
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime ModifiedAt { get; set; } 
    }
}
