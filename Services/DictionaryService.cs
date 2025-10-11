namespace WordWarrior.Services;

public record WordRecord(string Word, string Icon, int Level);

public class DictionaryService
{
    private readonly Random _random = new();
    private int? _lastIndex = null;

    public WordRecord Next(int level)
    {
        int index;
        
        // If we only have one word, return it (can't avoid repetition)
        if (_words.Length == 1)
        {
            return _words[0];
        }
        
        // Keep trying until we get a different word than the last one
        do
        {
            index = _random.Next(_words.Length);
        } while (index == _lastIndex);
        
        _lastIndex = index;
        return _words[index];
    }
    
    // hardcoded list of words
    private readonly WordRecord[] _words =
    [
        new WordRecord("zon", "☀️", 1),
        new WordRecord("bus", "🚌", 1),
        new WordRecord("maan", "🌙", 2),
        new WordRecord("ster", "⭐", 2),
        new WordRecord("vuur", "🔥", 2),
    ];
}

