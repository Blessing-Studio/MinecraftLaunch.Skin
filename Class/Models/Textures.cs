using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MinecraftLaunch.Skin.Class.Models {
    public class Textures {
        [JsonPropertyName("SKIN")]
        public SKIN Skin { get; set; }

        [JsonPropertyName("CAPE")]
        public CAPE Cape { get; set; }
    }
}
