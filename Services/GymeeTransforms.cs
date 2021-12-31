using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using rs = Intel.RealSense;

namespace GymeeDestkopApp.Services
{
    class GymeeTransforms
    {
        public static Bitmap GenerateRGBBitmap(rs.VideoFrame color)
        {
            return new Bitmap(
                color.Width,
                color.Height,
                color.Stride,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb,
                color.Data
            );
        }

        private static void RGBtoBGR(Bitmap bmp)
        {
            var data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                                        ImageLockMode.ReadWrite, bmp.PixelFormat);

            int length = Math.Abs(data.Stride) * bmp.Height;

            unsafe
            {
                byte* rgbValues = (byte*)data.Scan0.ToPointer();

                for (int i = 0; i < length; i += 3)
                {
                    byte dummy = rgbValues[i];
                    rgbValues[i] = rgbValues[i + 2];
                    rgbValues[i + 2] = dummy;
                }
            }

            bmp.UnlockBits(data);
        }

        public static void FixRealSenseBitmap(Bitmap bitmap)
        {
            RGBtoBGR(bitmap);
            bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
        }


        public static float[,] GenerateDepthFrameArray(rs.DepthFrame frame)
        {
            float[,] pixelMatrix = new float[frame.Width, frame.Height];
            for (Int16 x = 0; x < frame.Width; x++)
            {
                for (Int16 y = 0; y < frame.Height; y++)
                {
                    pixelMatrix[x, y] = frame.GetDistance(x, y);
                }
            }
            return pixelMatrix;
        }

        public static bool WriteDepthFrameToFile(rs.DepthFrame depth, string fileName)
        {
            var success = false;
            var fullFileName = $"{fileName}.ndp";
            using (StreamWriter writer = File.AppendText(fullFileName))
            {
                var depthUnitConstant = 0.001; //this is according to issue #8014 in librealsense go argue with MartyG-Realsense if you're bothered
                var depthDataArray = new Int16[depth.Height * depth.Width];
                Marshal.Copy(depth.Data, depthDataArray, 0, depth.DataSize / 2);
                writer.Write($"Camera = RealSense\nWidth = {depth.Width}\nHeight = {depth.Height}\nDepth_Unit = {depthUnitConstant}\nData = {String.Join(',',depthDataArray.Select(s => s.ToString("X")))}");
                success = true;
            }

            return success;
        }
        public static void SaveRgbPngs(List<Bitmap> bitmaps, string filePrefix)
        {
            int i = 1;
            foreach (var bitmap in bitmaps)
            {
                bitmap.Save($"{filePrefix}{i++}.png");
            }
        }
    }
}
