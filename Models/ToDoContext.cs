using Microsoft.EntityFrameworkCore;
using ToDoAPI.Models;

namespace TodoApi.Models;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Customer> Customer { get; set; } = null!;
    public DbSet<Measurement> Measurement { get; set; } = null!;
}
