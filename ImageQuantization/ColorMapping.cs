using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ImageQuantization.Data;

namespace ImageQuantization
{
    class ColorMapping
    {
        public static RGBPixel[,] NewColors(RGBPixel[,] ImageMatrix)
        {
            RGBPixel[,,] RGB = new RGBPixel[255,255,255];
            for (int x = 0; x < colors.Length; x++)
            {
                RGBPixel Clr_map = colorMap[x];
                RGB[colors[x].red, colors[x].green, colors[x].blue] = Clr_map;
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
