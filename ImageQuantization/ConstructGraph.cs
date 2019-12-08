using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ImageQuantization.Data;

namespace ImageQuantization
{
    class ConstructGraph
    {
        /// <summary>
        /// Calculate distance(edge weight) between the RGB values of the 2 vertices using the Euclidean Distance.
        /// </summary>
        /// <returns></returns>
        public void CalcDist()
        {
            distances = new double[colorsNum, colorsNum];
            for(int i = 0; i < colorsNum; i++)
            {
                for(int j = 0; j < colorsNum; j++)
                {
                    if (i != j)
                    {
                        distances[i, j] = Math.Sqrt((colors[j].red - colors[i].red) * (colors[j].red - colors[i].red) +
                                                    (colors[j].blue - colors[i].blue) * (colors[j].blue - colors[i].blue) +
                                                    (colors[j].green - colors[i].green) * (colors[j].green - colors[i].green));
                    }
                }
            }
        }
    }
}
