using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MinecraftLaunch.Skin.Class.Models {
    public class SkinMoreInfo {
        [JsonPropertyName("timestamp")]
        public long TimeStamp { get; set; }

        [JsonPropertyName("profileId")]
        public string ProfileId { get; set; }

        [JsonPropertyName("profileName")]
        public string ProfileName { get; set; }

        [JsonPropertyName("textures")]
        public Textures Textures { get; set; }
    }
}
