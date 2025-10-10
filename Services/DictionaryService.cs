namespace WordWarrior.Services;

public record WordRecord(string Word, string Icon);

public class DictionaryService
{
    private readonly Random _random = new();

    public WordRecord Next(int level)
    {
        return _words[_random.Next(_words.Length)];
    }
    
    // hardcoded list of words
    private readonly WordRecord[] _words =
    [
        new WordRecord("zon", "☀️"),
        new WordRecord("maan", "🌙")
    ];
}

