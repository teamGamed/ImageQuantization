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
                    double cost = distances[fColor, sColor];
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
            foreach (var newNode in tree[node])
            {
                if (!vis[newNode])
                {
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

        private static double standardDiv(int num)
        {
            double curMean = mean(num);
            double sum = 0;
            foreach (var edge in listEdges)
            {
                if(edge == null)
                    continue;
                sum += (edge.Item1 - curMean) * (edge.Item1 - curMean);
            }
            sum /= num-1;
            return Math.Sqrt(sum);
        }

        private static double mean(int num)
        {
            double sum = 0;
            foreach (var edge in listEdges)
            {
                if(edge == null)
                    continue;
                sum += edge.Item1;
            }
            return sum / num;
        }

        public static int getK()
        {
            getEdges();
            int num = listEdges.Count;
            double beforCut = standardDiv(num);
            removeEdge(num);
            double afterCur = standardDiv(--num);
            double red = Math.Abs(beforCut - afterCur);
            double lastRed;
            int k = 1;
            do
            {
                k++;
                beforCut = afterCur;
                removeEdge(num);
                afterCur = standardDiv(--num);
                lastRed = red;
                red = Math.Abs(beforCut - afterCur);
            } while (Math.Abs(lastRed - red) >= 0.0001);
            return k;
        }

        private static void removeEdge(int num)
        {
            double curMean = mean(num);
            double mx = 0;
            int m1 = -1;
            int m2 = -1;
            foreach (var edge in listEdges)
            {
                if (edge == null)
                    continue;
                if (Math.Abs(curMean - edge.Item1) > mx)
                {
                    m1 = edge.Item2;
                    m2 = edge.Item3;
                }
            }

            for (int i = 0; i < listEdges.Count; i++)
            {
                var edge = listEdges[i];
                if (edge == null)
                    continue;
                if (m1 == edge.Item2 && m2 == edge.Item3)
                {
                    listEdges[i] = null;
                }
            }
        }
    }
}