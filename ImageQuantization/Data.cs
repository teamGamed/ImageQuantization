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
    }
}
