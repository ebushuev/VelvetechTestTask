using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TodoApiDTO.DAL.Models;

namespace TodoApiDTO.DAL.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options): base(options) { }

        public DbSet<TodoItem> TodoItems {
            get;set;}
    }
}
