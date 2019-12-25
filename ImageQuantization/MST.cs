using System.Collections.Generic;
using System.ComponentModel;

namespace ImageQuantization
{
    public class MST
    {
        private static int V = 0;
        static int getSmallestNode(double[] cost, bool[] visited)
        {
            double mn = double.MaxValue;
            int idx = -1;
            for (int i = 0; i < V; i++)
            {
                if (!visited[i] && cost[i] < mn)
                {
                    mn = cost[i];
                    idx = i;
                }
            }
            return idx;
        }
        public static void getMST(double[,] g)
        {
            V = Data.colorsNum;
            int[] p = new int[V];
            double[] cost = new double[V];
            bool[] visited = new bool[V];
            for (int i = 0; i < V; i++)
            {
                visited[i] = false;
                cost[i] = double.MaxValue;
            }

            p[0] = -1;
            cost[0] = 0;
            for (int i = 0; i < V - 1; i++)
            {
                int smallest = getSmallestNode(cost, visited);
                visited[smallest] = true;
                for (int node = 0; node < V; node++)
                {
                    if (visited[node] == true) continue;
                    if (g[smallest, node] < cost[node])
                    {
                        p[node] = smallest;
                        cost[node] = g[smallest, node];
                    }
                }
            }
            for(int i = 0 ; i < V ; i++)
                Data.MSTList[i] = new List<int>();
            for (int i = 0; i < V; i++)
                Data.MSTList[p[i]].Add(i);
        }
    }
}