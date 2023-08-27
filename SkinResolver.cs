using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flurl.Http;

namespace MinecraftLaunch.Skin {
    public class SkinResolver {
        private byte[] ImageBytes = null!;

        public SkinResolver(string url) {
            ImageBytes = url.GetBytesAsync().Result;
        }

        public SkinResolver(byte[] bytes) {
            ImageBytes = bytes;
        }

        public SkinResolver(FileInfo filePath) {
            if (filePath.Exists) {
                ImageBytes = File.ReadAllBytes(filePath.FullName);
            } else {
                throw new FileNotFoundException();
            }
        }

        /// <summary>
        /// 裁剪皮肤图片头像
        /// </summary>
        /// <returns>裁剪后图片</returns>
        public Image<Rgba32> CropSkinHeadBitmap() {
            Image<Rgba32> head = (Image<Rgba32>)Image.Load(ImageBytes);
            head.Mutate(x => x.Crop(Rectangle.FromLTRB(8, 8, 16, 16)));

            Image<Rgba32> hat = (Image<Rgba32>)Image.Load(ImageBytes);
            hat.Mutate(x => x.Crop(Rectangle.FromLTRB(40, 8, 48, 16)));

            Image<Rgba32> endImage = new Image<Rgba32>(8, 8);
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    endImage[i, j] = head[i, j];
                    if (hat[i, j].A == 255) {
                        endImage[i, j] = hat[i, j];
                    }
                }
            }

            return ResizeImage(endImage, 60, 60);
        }

        /// <summary>
        /// 裁剪皮肤图片身体
        /// </summary>
        /// <typeparam name="TPixel"></typeparam>
        /// <param name="skin"></param>
        /// <returns></returns>
        public Image<Rgba32> CropSkinBodyBitmap() {
            var skin = ImageHelper.ConvertToImage(ImageBytes);

            Image<Rgba32> Body = CopyBitmap(skin);
            Body.Mutate(x => x.Crop(Rectangle.FromLTRB(20, 20, 28, 32)));
            return ResizeImage(Body, 60, 90);
        }

        /// <summary>
        /// 裁剪皮肤图片右手
        /// </summary>
        /// <typeparam name="TPixel"></typeparam>
        /// <param name="skin"></param>
        /// <returns></returns>
        public Image<Rgba32> CropRightHandBitmap() {
            var skin = ImageHelper.ConvertToImage(ImageBytes);

            Image<Rgba32> Arm = CopyBitmap(skin);
            Arm.Mutate(x => x.Crop(Rectangle.FromLTRB(35, 52, 39, 64)));
            return ResizeImage(Arm, 30, 90);
        }

        /// <summary>
        /// 裁剪皮肤图片左手
        /// </summary>
        /// <typeparam name="TPixel"></typeparam>
        /// <param name="skin"></param>
        /// <returns></returns>
        public Image<Rgba32> CropLeftHandBitmap() {
            var skin = ImageHelper.ConvertToImage(ImageBytes);

            Image<Rgba32> Arm = CopyBitmap(skin);
            Arm.Mutate(x => x.Crop(Rectangle.FromLTRB(44, 20, 48, 32)));
            return ResizeImage(Arm, 30, 90);
        }

        /// <summary>
        /// 裁剪皮肤图片右腿
        /// </summary>
        /// <typeparam name="TPixel"></typeparam>
        /// <param name="skin"></param>
        /// <returns></returns>
        public Image<Rgba32> CropRightLegBitmap() {
            var skin = ImageHelper.ConvertToImage(ImageBytes);

            Image<Rgba32> Leg = CopyBitmap(skin);
            Leg.Mutate(x => x.Crop(Rectangle.FromLTRB(20, 52, 24, 64)));
            return ResizeImage(Leg, 30, 90);
        }

        /// <summary>
        /// 裁剪皮肤图片左腿
        /// </summary>
        /// <typeparam name="TPixel"></typeparam>
        /// <param name="skin"></param>
        /// <returns></returns>
        public Image<Rgba32> CropLeftLegBitmap() {
            var skin = ImageHelper.ConvertToImage(ImageBytes);

            Image<Rgba32> Leg = CopyBitmap(skin);
            Leg.Mutate(x => x.Crop(Rectangle.FromLTRB(4, 20, 8, 32)));
            return ResizeImage(Leg, 30, 90);
        }

        /// <summary>
        /// 重置图片长宽
        /// </summary>
        /// <typeparam name="TPixel"></typeparam>
        /// <param name="image"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        public Image<Rgba32> ResizeImage(Image<Rgba32> image, int w, int h) {
            Image<Rgba32> image2 = new(w, h);
            for (int i = 0; i < w; i++) {
                for (int j = 0; j < h; j++) {
                    double tmp;
                    tmp = image.Width / (double)w;
                    double realW = tmp * (i);
                    tmp = image.Height / (double)h;
                    double realH = (tmp) * (j);
                    image2[i, j] = image[(int)realW, (int)realH];
                }
            }

            return image2;
        }

        /// <summary>
        /// 基础裁剪方法
        /// </summary>
        /// <typeparam name="TPixel"></typeparam>
        /// <param name="image"></param>
        /// <returns></returns>
        private Image<Rgba32> CopyBitmap(Image<Rgba32> image) {
            Image<Rgba32> tmp = new(image.Width, image.Height);
            for (int i = 0; i < image.Width; i++) {
                for (int j = 0; j < image.Height; j++) {
                    tmp[i, j] = image[i, j];
                }
            }

            return tmp;
        }
    }
}
