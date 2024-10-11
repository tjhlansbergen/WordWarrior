using Microsoft.EntityFrameworkCore;

namespace WordWarrior;

// using primary constructor
public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    // table(s)
    public DbSet<Sentence> Sentences { get; set; }
}

public class Sentence
{
    public int Id { get; set; }
    public required string Content { get; set; }
    public DateTime InsertedUtc { get; init; } = DateTime.UtcNow;
}