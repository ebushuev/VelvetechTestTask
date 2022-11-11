using Microsoft.EntityFrameworkCore;
using System;
using TodoApiDTO.DAL.Entities;

namespace TodoApiDTO.DAL.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>().HasData(
                new TodoItem
                {
                    Id = 1,
                    Name = "Name1",
                    IsComplete = true,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new TodoItem
                {
                    Id = 2,
                    Name = "Name2",
                    IsComplete = false,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new TodoItem
                {
                    Id = 3,
                    Name = "Name3",
                    IsComplete = false,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new TodoItem
                {
                    Id = 4,
                    Name = "Name4",
                    IsComplete = true,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new TodoItem
                {
                    Id = 5,
                    Name = "Name5",
                    IsComplete = true,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                }
            );
        }
    }
}
