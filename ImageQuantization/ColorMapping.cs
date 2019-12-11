using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class ColorMapping
    {
        public static RGBPixel[,] NewColors(RGBPixel[,] ImageMatrix)
        {
            RGBPixel[,,] RGB = new RGBPixel[260,260,260];
            for (int x = 0; x < Data.colorsNum; x++)
            {
                RGBPixel Clr_map = Data.colorMap[x];
                RGB[Data.colors[x].red, Data.colors[x].green, Data.colors[x].blue] = Clr_map;
            }         
            int length = ImageMatrix.GetLength(0);
            int width = ImageMatrix.GetLength(1);
            for (long i = 0; i < length; i++)
            {
                for (long j = 0; j < width; j++)
                {
                    RGBPixel new_Clr;
                    new_Clr= RGB[ImageMatrix[i, j].red, ImageMatrix[i, j].green, ImageMatrix[i, j].blue];
                    ImageMatrix[i, j].red = new_Clr.red;
                    ImageMatrix[i, j].green = new_Clr.green;
                    ImageMatrix[i, j].blue = new_Clr.blue;
                }
            }
            return ImageMatrix;
        }
    }
}
