
using Microsoft.EntityFrameworkCore;

namespace WordWarrior;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();
        app.UseHttpsRedirection();

        app.MapGet("/sentence", () =>
        {
            return "hi";
        });

        app.Run();
    }
}
