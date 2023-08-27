using SixLabors.ImageSharp.Formats.Png;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftLaunch.Skin {
    public static class ImageHelper {
        public static Image<Rgba32> ConvertToImage(byte[] raw) {
            return (Image<Rgba32>)Image.Load(raw);
        }

        public static Image<Rgba32> ConvertToImage(Stream raw) {
            return (Image<Rgba32>)Image.Load(raw);
        }

        public static byte[] ConvertToByteArray(Image<Rgba32> raw) {
            using var stream = new MemoryStream();
            raw.Save(stream, new PngEncoder());
            stream.Position = 0;
            return stream.ToArray();
        }

        public static MemoryStream ConvertToStream(Image<Rgba32> raw) {
            using var stream = new MemoryStream();
            raw.Save(stream, new PngEncoder());
            stream.Position = 0;
            return stream;
        }
    }
}