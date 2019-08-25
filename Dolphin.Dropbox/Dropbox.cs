using System;
using System.IO.Abstractions;
using Newtonsoft.Json;

namespace Dolphin.Dropbox
{
    public class Dropbox : IDropbox
    {
        private readonly IFileSystem _fileSystem;

        public Dropbox(IFileSystem fileSystem) => _fileSystem = fileSystem;

        public Dropbox() : this(new FileSystem())
        {
        }

        public string GetPersonalDirectory()
        {
            var home = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var infoFileName = _fileSystem.Path.Combine(home, "Dropbox", "info.json");
            var dropboxInfo =
                JsonConvert.DeserializeObject<DropboxInfo>(
                    _fileSystem.File.ReadAllText(infoFileName));
            return dropboxInfo.Personal.Path;
        }

        internal class DropboxInfo
        {
            [JsonProperty("personal")] public PersonalInfo Personal { get; set; }
        }

        internal class PersonalInfo
        {
            [JsonProperty("host")] public string Host { get; set; }

            [JsonProperty("is_team")] public bool IsTeam { get; set; }

            [JsonProperty("path")] public string Path { get; set; }

            [JsonProperty("subscription_type")] public string SubscriptionType { get; set; }
        }
    }
}