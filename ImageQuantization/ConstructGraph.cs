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
            bool[,,] vis = new bool[257, 257, 257];
            for (int i = 0; i < length; i++) 
                for (int j = 0; j < width; j++)
                    vis[ImageMatrix[i, j].blue, ImageMatrix[i, j].green, ImageMatrix[i, j].red] = true;
            
            for(int i = 0; i < 256; i++) 
                for (int j = 0; j < 256; j++) 
                    for (int k = 0; k < 256; k++) 
                        if (vis[i, j, k])
                            colorsNum++;
            
            
            colors = new RGBPixel[colorsNum];
            int count = 0;
            
            for(int i = 0; i < 256; i++) 
                for (int j = 0; j < 256; j++) 
                    for (int k = 0; k < 256; k++) 
                        if (vis[i, j, k]) {
                            var p = new RGBPixel();
                            p.blue = (byte)i;
                            p.green = (byte)j;
                            p.red = (byte)k;
                            colors[count++] = p;
                        }
                            
                    
                
            
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
