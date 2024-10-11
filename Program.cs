using Microsoft.EntityFrameworkCore;

namespace WordWarrior;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<SentenceService>();

        var app = builder.Build();
        app.UseHttpsRedirection();
        _setupEndpoints(app);

        app.Run();
    }

    private static void _setupEndpoints(WebApplication wapp)
    {
        wapp.MapGet("/random-sentence", async (SentenceService service) =>
        {
            return await service.GetRandomSentence();
        });
        
        wapp.MapPost("/sentence", async (SentenceService service, Sentence sentence) =>
        {
            return await service.AddSentence(sentence);
        });
    }
}
