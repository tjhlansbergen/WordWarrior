namespace WordWarrior.Services;

public class BackgroundService
{
    private readonly Random _random = new();
    
    // List of available background images
    private readonly string[] _backgroundImages = 
    [
        "1.jpg", "2.jpg", "3.jpg", "5.jpg", "6.jpg", 
        "7.jpg", "8.jpg", "9.jpg", "10.jpg", "11.jpg", 
        "12.jpg", "13.jpg", "14.jpg"
    ];

    public string GetRandomBackground()
    {
        var randomIndex = _random.Next(_backgroundImages.Length);
        return $"backgrounds/{_backgroundImages[randomIndex]}";
    }
}