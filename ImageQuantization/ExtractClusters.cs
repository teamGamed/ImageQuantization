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

        private static double mean()
        {
            double sum = 0;
            int count = 0;
            for (int i = 0; i < listEdges.Count; i++)
            {
                if(listEdges[i] == null)
                    continue;
                sum += listEdges[i].Item1;
                count++;
            }

            return sum / count;
        }

        private static double SD()
        {
            double M = mean();
            double sum = 0;
            int count = 0;
            for (int i = 0; i < listEdges.Count; i++)
            {
                if(listEdges[i] == null)
                    continue;
                sum += (listEdges[i].Item1 - M) * (listEdges[i].Item1 - M);
                count++;
            }

            sum /= count;
            return Math.Sqrt(sum);
        }

        private static double remove()
        {
            double M = mean();
            int m1 = -1;
            int m2 = -1;
            double mx = 0;
            double ans = 0;
            for (int i = 0; i < listEdges.Count; i++)
            {
                if(listEdges[i] == null)
                    continue;
                double cur = Math.Abs(M - listEdges[i].Item1);
                if (cur > mx)
                {
                    mx = cur;
                    ans = cur;
                    m1 = listEdges[i].Item2;
                    m2 = listEdges[i].Item3;
                }
            }
            for (int i = 0; i < listEdges.Count; i++)
            {
                if(listEdges[i] == null)
                    continue;
                if (m1 == listEdges[i].Item2 && m2 == listEdges[i].Item3)
                {
                    listEdges[i] = null;
                    break;
                }
            }

            return ans;
        }

        public static int getK()
        {
            getEdges();
            double r1 = SD();
            remove();
            double r2 = SD();
            int k = 1;
            while (Math.Abs(r1 - r2) >= 0.0001)
            {
                k++;
                r1 = r2;
                remove();
                r2 = SD();
            }
            return k;
        }
    }
}