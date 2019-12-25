using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ImageQuantization.Data;

namespace ImageQuantization
{
    class ConstructGraph
    {
        //find distnict colors in image matrix
        public static void Diffcolors(RGBPixel[,] ImageMatrix)
        {
            int length = ImageMatrix.GetLength(0);
            int width = ImageMatrix.GetLength(1);
            HashSet<RGBPixel> diffcolors = new HashSet<RGBPixel>();
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                    diffcolors.Add(ImageMatrix[i, j]);
            }
            colors = new RGBPixel[diffcolors.Count()];
            int count = 0;
            foreach (RGBPixel i in diffcolors)
            {
                colors[count++] = i;
            }
            colorsNum=diffcolors.Count();
        }
        /// <summary>
        /// Calculate distance(edge weight) between the RGB values of the 2 vertices using the Euclidean Distance, then construct
        /// the graph using adjacency list.
        /// </summary>
        /// <returns></returns>
        public static void CalcDist()
        {
            distances = new double[colorsNum, colorsNum];
            adj = new List<KeyValuePair<int, double>>[colorsNum];
            for (int i = 0; i < colorsNum; i++)
            {
                adj[i] = new List<KeyValuePair<int, double>>();
            }

            for (int i = 0; i < colorsNum; i++)
            {
                for (int j = 0; j < colorsNum; j++)
                {
                    if (i != j)
                    {
                        distances[i, j] = Math.Sqrt((colors[j].red - colors[i].red) * (colors[j].red - colors[i].red) +
                                                    (colors[j].blue - colors[i].blue) * (colors[j].blue - colors[i].blue) +
                                                    (colors[j].green - colors[i].green) * (colors[j].green - colors[i].green));
                        adj[i].Add(new KeyValuePair<int, double>(j, distances[i, j]));
                    }
                }
            }
        }
        
    }
}
