using Microsoft.EntityFrameworkCore;
using Todo.Data.Models;

public class TodosContext : DbContext
{
    public TodosContext()
    { }

    public TodosContext(DbContextOptions<TodosContext> options)
            : base(options)
        { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0");
        }
    }
    public DbSet<Todos> Todos { get; set; }

}