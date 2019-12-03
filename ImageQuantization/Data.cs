using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class Data
    {
        // have the distinct colors   
        public static List<RGBPixel> colors;
        // the graph
        public static Dictionary<RGBPixel , Dictionary<RGBPixel , double>> distances;
        // MST
        public static Dictionary< RGBPixel ,List<RGBPixel>> MST;
        // the components after Extract the clusters
        public static List<List<RGBPixel>> comps;
        // the mapping color
        public static Dictionary<RGBPixel, RGBPixel> colorMap;
    }
}
