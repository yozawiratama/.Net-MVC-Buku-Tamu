using Microsoft.EntityFrameworkCore;
using BukuTamuTest.Models;

namespace BukuTamuTest;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public DbSet<Tamu> Tamus { get; set; }
}