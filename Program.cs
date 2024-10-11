using Microsoft.EntityFrameworkCore;

namespace WordWarrior;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddScoped<SentenceService>();
        builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();
        app.UseHttpsRedirection();
        SetupEndpoints(app);

        app.Run();
    }

    private static void SetupEndpoints(WebApplication wapp)
    {
        wapp.MapGet("/random-sentence", (SentenceService service) =>
            service.GetRandomSentence());

        wapp.MapPost("/sentence", (SentenceService service, Sentence sentence) =>
            service.AddSentence(sentence));
    }
}
