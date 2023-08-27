using Flurl;
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
    public class YggdrasilSkinFetcher : IFetcher {
        private string Uuid;

        public string BaseApi = string.Empty;

        public YggdrasilSkinFetcher(string url, string uuid) {
            Uuid = uuid;
            BaseApi = $"{url}/sessionserver/session/minecraft/profile/{uuid.Replace("-", string.Empty)}";
        }

        public async ValueTask<byte[]> GetSkinAsync() {
            string json = await BaseApi.GetStringAsync();
            var skinjson = Encoding.UTF8.GetString(Convert.FromBase64String(JsonSerializer.Deserialize<AccountSkinModel>(json)!.Properties.First().Value));
            var url = JsonSerializer.Deserialize<SkinMoreInfo>(skinjson)!.Textures.Skin.Url;
            return await url.GetBytesAsync();
        }

        public void Save() {
            throw new NotImplementedException();
        }
    }
}
