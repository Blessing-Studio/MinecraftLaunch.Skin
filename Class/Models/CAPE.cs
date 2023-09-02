using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MinecraftLaunch.Skin.Class.Models {
    public record CAPE {
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
