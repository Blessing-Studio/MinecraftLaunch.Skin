using Flurl.Http;
using MinecraftLaunch.Skin.Class.Interfaces;
using MinecraftLaunch.Skin.Class.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MinecraftLaunch.Skin.Class.Fetchers {
    public class UnifiedPassSkinFetcher : IFetcher {
        private string Uuid;

        public static readonly string BaseApi = "https://auth.mc-user.com:233/";

        public string ServerId { get; set; } = string.Empty;

        public UnifiedPassSkinFetcher(string serverId,string uuid) {
            Uuid = uuid;
            ServerId = serverId;
        }

        public async ValueTask<byte[]> GetSkinAsync() {
            var baseApi = $"{BaseApi}{ServerId}/sessionserver/session/minecraft/profile/{Uuid}";
            string json = await baseApi.GetStringAsync();
            var skinjson = Encoding.UTF8.GetString(Convert.FromBase64String(JsonSerializer.Deserialize<AccountSkinModel>(json)!.Properties.First().Value));
            var url = JsonSerializer.Deserialize<SkinMoreInfo>(skinjson)!.Textures.Skin.Url;
            return await url.GetBytesAsync();
        }

        public void Save() {
            throw new NotImplementedException();
        }
    }
}
