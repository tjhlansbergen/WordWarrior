
using Microsoft.EntityFrameworkCore;

namespace WordWarrior;

// using primary constructor
public class SentenceService(DataContext db)
{
    private readonly DataContext _db = db;

    public async Task<IResult> AddSentence(Sentence sentence)
    {
        _db.Sentences.Add(sentence);
        await _db.SaveChangesAsync();
        return Results.Created($"/sentence/{sentence.Id}", sentence);
    }

    public async Task<IResult> GetRandomSentence()
    {
        var sentences = await _db.Sentences.ToListAsync();

        var count = sentences.Count;
        return (count == 0)
            ? Results.NotFound()
            : Results.Ok(sentences[new Random().Next(count)]);
    }
}