using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class Data
    {
        // --- initialize the data structure before store in it --- 

        // count of distinct colors
        public static int colorsNum;
        // distinct colors | the indx will be used as id to the color to the rest in classes 
        public static RGBPixel[] colors;
        // the graph
        public static double[,] distances;
        // MST
        public static List<int>[] MSTList;
        // the components after Extract the clusters
        public static List<List<int>> comps;
        // the mapping color
        public static RGBPixel[] colorMap;
        // adjacency list 
        public static List<KeyValuePair<int, double>>[] adj;

        public static RGBPixel[,] get(RGBPixel[,] ImageMatrix)
        {
            for(int i = 0; i < ImageMatrix.GetLength(0); i++)
            {
                for(int j = 0; j < ImageMatrix.GetLength(1); j++)
                {
                    for(int k = 0; k < Data.colorsNum; k++)
                    {
                        if(Data.colors[k].blue == ImageMatrix[i, j].blue && Data.colors[k].green == ImageMatrix[i, j].green &&
                            Data.colors[k].red == ImageMatrix[i, j].red )
                        {
                            ImageMatrix[i, j] = Data.colorMap[k];
                        }
                    }
                }
            }
            return ImageMatrix;
        }
        
    }
}
