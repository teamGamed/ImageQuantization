using System;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using Microsoft.SqlServer.Server;
using Priority_Queue;
using static ImageQuantization.Data;

namespace ImageQuantization
{
    class MST
    {
        /// <summary>
        /// Construct MST using prim algorithm 
        /// </summary>
        /// <returns></returns>
        public static void getMST()
        {
            int vNum = colorsNum;
            bool[] visited = new bool[vNum];
            int[] parent = new int[vNum];
            double[] dist = new double[vNum];
            for (int i = 0; i < vNum; i++)
            {
                parent[i] = -1;
                dist[i] = 1e18;
                visited[i] = false;
            }


            SimplePriorityQueue<int> pq = new SimplePriorityQueue<int>();
            pq.Enqueue(0, 0);
            dist[0] = 0;
            while (pq.Count > 0)
            {
                int u = pq.Dequeue();
                visited[u] = true;
                for (int i = 0; i < adj[u].Count; i++)
                {
                    int child = adj[u][i].Key;
                    double w = adj[u][i].Value;
                    if (!visited[child] && w < dist[child])
                    {
                        parent[child] = u;
                        dist[child] = w;
                        pq.Enqueue(child, (float)w);
                    }
                }
            }

            MSTList = new List<int>[vNum];
            for (int i = 0; i < vNum; i++)
            {
                MSTList[i] = new List<int>();
            }

            for (int i = 1; i < vNum; i++)
            {
                MSTList[i].Add(parent[i]);
            }
            
        }
    }
}
