using System.Globalization;
using System.Text.Json;
using CashFlow.Domain.Interfaces;

namespace CashFlow.Infrastructure.Resources;

internal class JsonResourceAccessor : IResourceAccessor
{
    private readonly Dictionary<string, JsonElement> _resources = [];
    private readonly JsonSerializerOptions _jsonOptions;

    public JsonResourceAccessor()
    {
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true
        };

        var assemblyLocation = typeof(JsonResourceAccessor).Assembly.Location;
        var assemblyDirectory = Path.GetDirectoryName(assemblyLocation);

        if (string.IsNullOrEmpty(assemblyDirectory))
        {
            throw new InvalidOperationException("Unable to determine assembly directory.");
        }

        var resourcesPath = Path.Combine(assemblyDirectory, "Resources");

        if (!Directory.Exists(resourcesPath))
        {
            throw new DirectoryNotFoundException($"Resources directory not found: {resourcesPath}");
        }

        foreach (var file in Directory.GetFiles(resourcesPath, "*.json"))
        {
            var cultureName = Path.GetFileNameWithoutExtension(file);
            LoadResources(file, cultureName);
        }
    }

    private void LoadResources(string filePath, string cultureName)
    {
        if (File.Exists(filePath))
        {
            var jsonString = File.ReadAllText(filePath);
            var jsonDocument = JsonSerializer.Deserialize<JsonElement>(jsonString, _jsonOptions);

            _resources[cultureName] = jsonDocument;
        }
    }

    public string GetString(string key)
    {
        var culture = CultureInfo.CurrentUICulture.Name;

        if (!_resources.TryGetValue(culture, out var resourceSet) &&
            !_resources.TryGetValue(culture.Split('-')[0], out resourceSet) &&
            !_resources.TryGetValue("en", out resourceSet))
        {
            return key; // Fallback final para a chave original
        }

        var keys = key.Split('.');
        var currentElement = resourceSet;

        foreach (var subKey in keys)
        {
            if (!currentElement.TryGetProperty(subKey, out var nextElement))
            {
                return key;
            }
            currentElement = nextElement;
        }

        return currentElement.ValueKind == JsonValueKind.String ? currentElement.GetString() ?? key : key;
    }
}
