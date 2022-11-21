﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    public class TodoItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string Secret { get; set; }
    }
}