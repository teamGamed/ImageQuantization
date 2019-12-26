using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ImageQuantization.Data;

namespace ImageQuantization
{
    class ExtractClusters
    {
        private static bool[] vis;
        private static List<int>[] tree;
        private static List<Tuple<double, int, int>> listEdges;
        //private static double[] cost;
        /// <summary>
        /// get all the edges from Data.MSTList and sort them descending 
        /// </summary>
        /// <returns></returns>
        private  static void getEdges()
        {
            var edges = new List<Tuple<double, int, int>>();
            var vis = new Dictionary<int , HashSet<int>>();
            for(int i = 0;i< colorsNum; i++)
            {
                vis.Add( i , new HashSet<int>() );
            }
            for (int i = 0; i < colorsNum; i++)
            {
                for (int j = 0; j < MSTList[i].Count; j++)
                {
                    int fColor = i;
                    int sColor = MSTList[i][j];
                    if (vis[fColor].Contains(sColor))
                        continue;
                    vis[fColor].Add(sColor);
                    vis[sColor].Add(fColor);
                    double cost = getDis(fColor , sColor);
                    var val = new Tuple<double , int , int>(cost , fColor , sColor);
                    edges.Add(val);
                }
            }
            edges.Sort();
            edges.Reverse();
            listEdges = edges;
        }
        /// <summary>
        /// get the tree after separates it to $compNum component
        /// </summary>
        /// <param name="compNum">the number of component in the new tree</param>
        /// <returns></returns>
        private static List<int>[] getupdatedTree(int compNum)
        {
            getEdges();
            int removedEdges = Math.Min(compNum - 1, listEdges.Count);
            List<int>[] updatedTree = new List<int>[colorsNum];
            for(int i = 0; i < colorsNum; i++)
            {
                updatedTree[i] = new List<int>();
            }
            for (int i = removedEdges; i < listEdges.Count; i++)
            {
                int fColor = listEdges[i].Item2;
                int sColor = listEdges[i].Item3;
                updatedTree[fColor].Add(sColor);
                updatedTree[sColor].Add(fColor);
            }
            return updatedTree;
        }
        private static void dfs(int node , ref List<int> curComp)
        {
            vis[node] = true;
            curComp.Add(node);
            foreach (var newNode in tree[node]) {
                if (!vis[newNode]) {
                    dfs(newNode , ref curComp);
                }
            }
        }
        /// <summary>
        /// extract the k Clusters from Data.MSTList and store the new Componentes in Data.comps
        /// </summary>
        /// <param name="k"></param>
        public static void extractClusters(int k)
        {
            tree = getupdatedTree(k);
            vis = new bool[colorsNum];
            comps = new List<List<int>>();
            for (int i = 0; i < colorsNum; i++)
            {
                if (vis[i])
                    continue;
                var curComp = new List<int>();
                dfs(i, ref curComp);
                comps.Add(curComp);
            }
        }
        /// <summary>
        /// Find the representative color of each cluster.
        /// </summary>
        public static void getClustersColors()
        {
            colorMap = new RGBPixel[Data.colorsNum];
            for (int i=0;i<comps.Count;i++)
            {
                int red = 0, blue = 0, green = 0, clusterCount;
                for (int j=0;j<comps[i].Count;j++)
                {
                    red += colors[comps[i][j]].red;
                    blue += colors[comps[i][j]].blue;
                    green += colors[comps[i][j]].green;
                }
                clusterCount = comps[i].Count;
                RGBPixel newColor;
                newColor.red = (byte) (red/clusterCount);
                newColor.blue = (byte)(blue / clusterCount);
                newColor.green = (byte)(green / clusterCount);
                for (int j = 0; j < comps[i].Count; j++)
                {
                    colorMap[comps[i][j]] = newColor;
                }
            }
        }

        // private static double mean()
        // {
        //     double sum = 0;
        //     int count = 0;
        //     for (int i = 0; i < listEdges.Count; i++)
        //     {
        //         if(listEdges[i] == null)
        //             continue;
        //         sum += listEdges[i].Item1;
        //         count++;
        //     }
        //
        //     return sum / count;
        // }
        //
        // private static double SD()
        // {
        //     double M = mean();
        //     double sum = 0;
        //     int count = 0;
        //     for (int i = 0; i < listEdges.Count; i++)
        //     {
        //         if(listEdges[i] == null)
        //             continue;
        //         sum += (listEdges[i].Item1 - M) * (listEdges[i].Item1 - M);
        //         count++;
        //     }
        //     return Math.Sqrt(sum  / count);
        // }
        //
        // private static void remove()
        // {
        //     double M = mean();
        //     int m1 = -1;
        //     int m2 = -1;
        //     double mx = 0;
        //     double ans = 0;
        //     for (int i = 0; i < listEdges.Count; i++)
        //     {
        //         if(listEdges[i] == null)
        //             continue;
        //         double cur = Math.Abs(M - listEdges[i].Item1);
        //         if (cur > mx)
        //         {
        //             mx = cur;
        //             ans = cur;
        //             m1 = listEdges[i].Item2;
        //             m2 = listEdges[i].Item3;
        //         }
        //     }
        //     for (int i = 0; i < listEdges.Count; i++)
        //     {
        //         if(listEdges[i] == null)
        //             continue;
        //         if (m1 == listEdges[i].Item2 && m2 == listEdges[i].Item3)
        //         {
        //             listEdges[i] = null;
        //             break;
        //         }
        //     }
        // }
        //
        // public static int getK()
        // {
        //     getEdges();
        //     double r1 = SD();
        //     remove();
        //     double r2 = SD();
        //     // double dif = Math.Abs(r1 - r2);
        //     // double last;
        //     int k = 0;
        //     while (Math.Abs(r1 - r2) >= 0.0001)
        //     {
        //         k++;
        //         r1 = r2;
        //         remove();
        //         r2 = SD();
        //     }
        //     return k;
        // }
        //
        // public static void rem()
        // {
        //     int m1 = -1;
        //     int m2 = -1;
        //     double mn = Double.MaxValue;
        //     for (int i = 0; i < listEdges.Count; i++)
        //     {
        //         var edge = listEdges[i];
        //         if(listEdges[i] == null)
        //             continue;
        //         
        //         listEdges[i] = null;
        //         double cur = SD();
        //         listEdges[i] = edge;
        //         
        //         if (cur < mn)
        //         {
        //             mn = cur;
        //             m1 = listEdges[i].Item2;
        //             m2 = listEdges[i].Item3;
        //         }
        //     }
        //     for (int i = 0; i < listEdges.Count; i++)
        //     {
        //         if(listEdges[i] == null)
        //             continue;
        //         if (m1 == listEdges[i].Item2 && m2 == listEdges[i].Item3)
        //         {
        //             listEdges[i] = null;
        //             break;
        //         }
        //     }
        // }

        public static int getKK(){
            getEdges();
            double[] arr = new double[listEdges.Count];
            for (int i = 0; i < listEdges.Count; i++)
            {
                arr[i] = listEdges[i].Item1;
            }
            int pt1 = 0, pt2 = listEdges.Count - 1;
            int n = listEdges.Count;
            int k = 0;
            bool f = true;
            while (pt1 < pt2)
            {
                double mean = 0;
                for (int i = pt1; i <= pt2; i++)
                {
                    mean += arr[i];
                }
                mean /= (pt2 - pt1 + 1);
                double oldSD = 0; /// before cut
                for (int i = pt1; i <= pt2; i++)
                {
                    oldSD += (arr[i] - mean ) * (arr[i] - mean ) ;
                }
                oldSD /= (pt2 - pt1 + 1);
                oldSD = Math.Sqrt(oldSD);

                if (Math.Abs(mean - arr[pt1]) > Math.Abs(mean - arr[pt2]))
                {
                    pt1++;
                }
                else
                {
                    pt2--;
                }
                
                double newMean = 0;
                for (int i = pt1; i <= pt2; i++)
                {
                    newMean += arr[i];
                }
                newMean /= (pt2 - pt1 + 1);
                double newSD = 0; /// before cut
                for (int i = pt1; i <= pt2; i++)
                {
                    newSD += (arr[i] - newMean ) * (arr[i] - newMean ) ;
                }
                newSD /= (pt2 - pt1 + 1);
                newSD = Math.Sqrt(newSD);

                if (Math.Abs(oldSD - newSD) < 0.0001 && (pt2 - pt1 + 1) <= 2*n/3.0 )
                {
                    break;
                }
                k++;
            }
            return k;
        }
    }
}