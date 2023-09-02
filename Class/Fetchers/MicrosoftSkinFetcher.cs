using Flurl.Http;
using MinecraftLaunch.Skin.Class.Interfaces;
using MinecraftLaunch.Skin.Class.Models;
using System.Text;
using System.Text.Json;

namespace MinecraftLaunch.Skin.Class.Fetchers {
    public class MicrosoftSkinFetcher : IFetcher {
        private string Uuid;

        public static readonly string BaseApi = "https://sessionserver.mojang.com/session/minecraft/profile/";

        public MicrosoftSkinFetcher(string uuid) { 
            Uuid = uuid;
        }

        public async ValueTask<byte[]> GetSkinAsync() {
            string json = await $"{BaseApi}{Uuid}".GetStringAsync();
            var skinjson = Encoding.UTF8.GetString(Convert.FromBase64String(JsonSerializer.Deserialize<AccountSkinModel>(json)!.Properties.First().Value));
            string url = JsonSerializer.Deserialize<SkinMoreInfo>(skinjson)!.Textures.Skin.Url;
            return await url.GetBytesAsync();
        }

        public void Save() {
            throw new NotImplementedException();
        }
    }
}