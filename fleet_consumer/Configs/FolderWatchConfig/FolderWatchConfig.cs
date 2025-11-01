using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Configs.FolderWatch;

public sealed class FolderWatchConfig
{
	[JsonPropertyName("path")]
	public string? Path { get; set; }

	[JsonPropertyName("fileTypes")]
	public string? FileType{ get; set; }
	 
	[JsonPropertyName("includeSubdirectories")]
	public bool? IncludeSubdirectories { get; set; }

	public void Validate()
	{
		if (string.IsNullOrEmpty(Path))
			throw new ArgumentException("Config path is required.");

		if (!Directory.Exists(Path!)) // Path can't be null, checked above
			throw new DirectoryNotFoundException("Directory not found.");

		if (string.IsNullOrEmpty(FileType))
			throw new ArgumentException("Filetype is not valid!");
	}

	public static FolderWatchConfig LoadJsonFromFile(string jsonFile)
	{
		if (!File.Exists(jsonFile))
			throw new FileNotFoundException("Config file not found.");

        var json = File.ReadAllText(jsonFile);
		var cfg = JsonSerializer.Deserialize<FolderWatchConfig>(json, JsonOptions()) ??
			throw new InvalidOperationException("Deserialization failed");

		cfg.Validate();
		return cfg;
	}

	internal static JsonSerializerOptions JsonOptions()
	{
		return new JsonSerializerOptions
		{
            // https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions?view=net-9.0
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			ReadCommentHandling = JsonCommentHandling.Skip,
		};
	}
}