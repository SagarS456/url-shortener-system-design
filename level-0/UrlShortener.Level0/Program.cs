using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.MapGet("/", () => "Hello World!");
app.MapPost("/shorten", (string url) =>
{
         var result = new StringBuilder(6);
   if (string.IsNullOrEmpty(url))
    {
        return $"URL cannot be empty.";
    }
    string path=@"D:\Sagar\Projects\SystemDesign\url-shortener-system-design\level-0\UrlShortener.Level0\Urls.json";
 
     if (!File.Exists(path))
{
    File.WriteAllText(path, "[]");
}
  var json = File.ReadAllText(path);

var urlMappings = JsonSerializer.Deserialize<List<UrlMapping>>(json);
const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
var random = new Random();
var shortCode = new string(
    Enumerable.Range(0, 6)
              .Select(_ => chars[random.Next(chars.Length)])
              .ToArray()
);
var newMapping = new UrlMapping
{
    ShortCode = shortCode,
    OriginalUrl = url,
    CreatedAt = DateTime.UtcNow
};

if (urlMappings == null)
{
    urlMappings = new List<UrlMapping>();
}

urlMappings.Add(newMapping);
var updatedJson = JsonSerializer.Serialize(urlMappings);

File.WriteAllText(path, updatedJson);


    // Logic to shorten the URL would go here
    return $"http://localhost:5196/{shortCode}";

});
app.MapGet("/{code}", (string code) =>
{
    string path = @"D:\Sagar\Projects\SystemDesign\url-shortener-system-design\level-0\UrlShortener.Level0\Urls.json";

    if (!File.Exists(path))
    {
        return Results.NotFound("No such website exists");
    }

    var json = File.ReadAllText(path);
    var urlMappings = JsonSerializer.Deserialize<List<UrlMapping>>(json);

    if (urlMappings == null)
    {
        return Results.NotFound("No such website exists");
    }

    var match = urlMappings.FirstOrDefault(x => x.ShortCode == code);

    if (match == null)
    {
        return Results.NotFound("No such website exists");
    }

    return Results.Redirect(match.OriginalUrl);
});


app.Run();

public class UrlMapping
{
    public string ShortCode { get; set; }
    public string OriginalUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}
