using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoApi.DAL.Models;

namespace TodoApi.DAL.DataContext;

public partial class TodoApiContext : DbContext
{
    private readonly ILoggerFactory _loggerFactory;
    
    public TodoApiContext()
    {
    }

    public TodoApiContext(DbContextOptions<TodoApiContext> options, ILoggerFactory loggerFactory)
        : base(options)
    {
        _loggerFactory = loggerFactory;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder
            .UseLoggerFactory(_loggerFactory)
            .UseSqlServer("Server=T1KZZ\\SQLEXPRESS;Database=TodoApi;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<TodoItemEntity> TodoItems { get; set; }
}
