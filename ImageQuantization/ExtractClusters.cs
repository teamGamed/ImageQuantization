using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ImageQuantization.Data;

namespace ImageQuantization
{
    class ExtractClusters
    {

        private bool[] vis;
        private List<int>[] tree;
        /// <summary>
        /// get all the edges from Data.MST and sort them descending 
        /// </summary>
        /// <returns></returns>
        private List<Tuple<double , int , int>> getEdges()
        {
            var edges = new List<Tuple<double, int, int>>();
            var vis = new bool[colorsNum, colorsNum];
            for (int i = 0; i < colorsNum; i++)
            {
                for (int j = 0; j < MST[i].Count; j++)
                {
                    int fColor = i;
                    int sColor = MST[i][j];
                    if (vis[fColor, sColor])
                        continue;
                    vis[fColor, sColor] = vis[sColor, fColor] = true;
                    double cost = distances[fColor, sColor];
                    var val = new Tuple<double , int , int>(cost , fColor , sColor);
                    edges.Add(val);
                }
            }
            edges.Sort();
            edges.Reverse();
            return edges;
        }
        /// <summary>
        /// get the tree after separates it to $compNum component
        /// </summary>
        /// <param name="compNum">the number of component in the new tree</param>
        /// <returns></returns>
        private List<int>[] getupdatedTree(int compNum)
        {
            var edges = getEdges();
            int removedEdges = Math.Min(compNum - 1, edges.Count);
            List<int>[] updatedTree = new List<int>[colorsNum];
            for (int i = removedEdges; i < edges.Count; i++)
            {
                int fColor = edges[i].Item2;
                int sColor = edges[i].Item3;
                updatedTree[fColor].Add(sColor);
                updatedTree[sColor].Add(fColor);
            }
            return updatedTree;
        }
        private void dfs(int node , ref List<int> curComp)
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
        /// extract the k Clusters from Data.MST and store the new Componentes in Data.comps
        /// </summary>
        /// <param name="k"></param>
        public void extractClusters(int k)
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
    }
}
