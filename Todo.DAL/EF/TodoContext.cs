using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Todo.DAL.EntityConfigurations;
using TodoApi.DAL.Entities;

namespace TodoApi.DAL.EF
{
    public class TodoContext : DbContext/////акакака
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TodoItemEntityConfigurationType());
            base.OnModelCreating(modelBuilder);
        }
    }
}
