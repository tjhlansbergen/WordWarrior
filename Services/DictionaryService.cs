using System.Text;
using YamlDotNet.Serialization;

namespace WordWarrior.Services;

public record WordRecord(string Word, string Icon, int Level);

public class WordsData
{
    [YamlMember(Alias = "words")]
    public List<WordYaml> Words { get; set; } = new();
}

public class WordYaml
{
    [YamlMember(Alias = "word")]
    public string Word { get; set; } = "";
    
    [YamlMember(Alias = "icon")]
    public string Icon { get; set; } = "";
    
    [YamlMember(Alias = "level")]
    public int Level { get; set; }
}

public class DictionaryService
{
    private readonly Random _random = new();
    private readonly HttpClient _httpClient;
    private int? _lastIndex = null;
    private WordRecord[]? _words;

    public DictionaryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WordRecord> NextAsync(int level)
    {
        if (_words == null)
        {
            await LoadWordsAsync();
        }

        if (_words?.Length == 0)
        {
            throw new InvalidOperationException("No words available");
        }

        int index;
        
        // If we only have one word, return it (can't avoid repetition)
        if (_words!.Length == 1)
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

    private async Task LoadWordsAsync()
    {
        try
        {
            var yamlContent = await _httpClient.GetStringAsync("data/configuration.yaml");
            var deserializer = new DeserializerBuilder()
                .IgnoreUnmatchedProperties()
                .Build();
            var wordsData = deserializer.Deserialize<WordsData>(yamlContent);
            
            if (wordsData?.Words != null && wordsData.Words.Count > 0)
            {
                _words = wordsData.Words
                    .Select(w => new WordRecord(w.Word, w.Icon, w.Level))
                    .ToArray();
            }
            else
            {
                throw new InvalidOperationException("No words found in YAML file");
            }
        }
        catch (Exception ex)
        {
            // Log the error for debugging (in production, use proper logging)
            Console.WriteLine($"Failed to load YAML: {ex.Message}");
        }
    }
}

