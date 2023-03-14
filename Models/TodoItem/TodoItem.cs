﻿using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    #region snippet
    public class TodoItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string Secret { get; set; }
    }
    #endregion
}