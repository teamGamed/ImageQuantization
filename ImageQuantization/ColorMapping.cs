using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ImageQuantization.Data;

namespace ImageQuantization
{
    class ColorMapping
    {
        public static void NewColors(RGBPixel[,] ImageMatrix)
        {
            Dictionary<RGBPixel, int> ConvertDict = new Dictionary<RGBPixel, int>();
            for (int x = 0; x < colors.Length; x++)
            {
                ConvertDict.Add(colors[x], x);
            }         
            int length = ImageMatrix.GetLength(0);
            int width = ImageMatrix.GetLength(1);
            for (long i = 0; i < length; i++)
            {
                for (long j = 0; j < width; j++)
                {
                    int id = ConvertDict[ImageMatrix[i, j]];
                    ImageMatrix[i, j].red = colorMap[id].red;
                    ImageMatrix[i, j].green = colorMap[id].green;
                    ImageMatrix[i, j].blue = colorMap[id].blue;
                }
            }

        }
    }
}
