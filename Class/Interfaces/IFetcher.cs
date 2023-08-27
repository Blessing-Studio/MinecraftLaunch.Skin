using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftLaunch.Skin.Class.Interfaces {
    public interface IFetcher {
        void Save();

        ValueTask<byte[]> GetSkinAsync();
    }
}
