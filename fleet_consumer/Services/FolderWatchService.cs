using System;
using System.IO;

using Configs.FolderWatch;

var cfg = FolderWatchConfig.LoadJsonFromFile("FolderWatchConfig.json");
var folderWatch = new FileSystemWatcher(cfg.Path!)
{
    Filter = cfg.FileType,
    IncludeSubdirectories = cfg.IncludeSubdirectories ?? false,
    EnableRaisingEvents = true
}
