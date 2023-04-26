using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using TodoCore.Data.Entities;

namespace TodoInfrastructure.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
