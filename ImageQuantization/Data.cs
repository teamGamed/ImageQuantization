using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class Data
    {
        // --- initialize the data structure before store in it --- 

        public static void clear()
        {
            colors = null;
            distances = null;
            MSTList = null;
            comps = null;
            colorMap = null;
            adj = null;
            sum = 0;
            colorsNum = 0;
        }
        // count of distinct colors
        public static int colorsNum = 0;
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

        public static double sum = 0;
        
        public static int getDis2(int i , int j)
        {
            return (colors[j].red - colors[i].red) * (colors[j].red - colors[i].red) +
                   (colors[j].blue - colors[i].blue) * (colors[j].blue - colors[i].blue) +
                   (colors[j].green - colors[i].green) * (colors[j].green - colors[i].green);
        }
    }
    
}
